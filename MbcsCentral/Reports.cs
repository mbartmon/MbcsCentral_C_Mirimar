using System;
using System.Drawing;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Printing;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using MbcsCentral;

public class Reports
{
	
	protected FixedDocument doc;
	
	private int pg = 0;
	private int maxLines = 0;
	private object lastGroup;
	private Type itmType = null;
	
	private enum PageOrientations
	{
		Portrait = 0,
		Landscape = 1
	}
	private const int MAX_PAGE_ORIENTATION = (int)PageOrientations.Landscape;
	
	private enum PaperSizes
	{
		Letter = 0,
		Legal = 1
	}
	private const int MAX_PAPER_SIZE = (int)PaperSizes.Legal;
	
	private System.Windows.Size LETTER_PORTRAIT = new System.Windows.Size(96 * 8.5, 96 * 11);
	private System.Windows.Size LETTER_LANDSCAPE = new System.Windows.Size(96 * 11, 96 * 8.5);
	private System.Windows.Size LEGAL_PORTRAIT = new System.Windows.Size(96 * 8.5, 96 * 14);
	private System.Windows.Size LEGAL_LANDSCAPE = new System.Windows.Size(96 * 14, 96 * 8.5);
	private System.Windows.Size[,] PAGE_SIZES; // VBConversions Note: Initial value of "new System.Windows.Size[,] {{LETTER_PORTRAIT, LETTER_LANDSCAPE}, {LEGAL_PORTRAIT, LEGAL_LANDSCAPE}}" cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
	
	// Properties
	protected int PAPER_SIZE = System.Convert.ToInt32(PaperSizes.Letter);
	protected short PAGE_ORIENTATION = System.Convert.ToInt16(PageOrientations.Portrait);
	protected System.Windows.Size PAGE_SIZE; // VBConversions Note: Initial value of "LETTER_PORTRAIT" cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
	protected float LINE_HEIGHT = System.Convert.ToSingle(0.167 * 96);
	protected float TOP_MARGIN = System.Convert.ToSingle(96 * 0.75);
	protected float BOTTOM_MARGIN = System.Convert.ToSingle(96 * 0.75);
	protected float LEFT_MARGIN = System.Convert.ToSingle(96 * 0.75);
	protected float RIGHT_MARGIN = System.Convert.ToSingle(96 * 0.75);
	protected float MAX_HEIGHT; // VBConversions Note: Initial value of "System.Convert.ToSingle(PAGE_SIZE.Height - System.Convert.ToSingle(96 * 0.75) - System.Convert.ToSingle(96 * 0.75))" cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
	
	private string _subName;
	private System.Windows.Window _parent;
	private bool _useGroups = false;
	private ArrayList _records;
	private Headers[] _header;
	private float _currentHeight = 0;
	
	private object _delegate;
	
	public Reports()
	{
		// VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
		PAGE_SIZES = new System.Windows.Size[,] {{LETTER_PORTRAIT, LETTER_LANDSCAPE}, {LEGAL_PORTRAIT, LEGAL_LANDSCAPE}};
		PAGE_SIZE = LETTER_PORTRAIT;
		MAX_HEIGHT = System.Convert.ToSingle(PAGE_SIZE.Height - System.Convert.ToSingle(96 * 0.75) - System.Convert.ToSingle(96 * 0.75));
		
		
	}
	
	public Reports(System.Windows.Window parent, string subName)
	{
		// VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
		PAGE_SIZES = new System.Windows.Size[,] {{LETTER_PORTRAIT, LETTER_LANDSCAPE}, {LEGAL_PORTRAIT, LEGAL_LANDSCAPE}};
		PAGE_SIZE = LETTER_PORTRAIT;
		MAX_HEIGHT = System.Convert.ToSingle(PAGE_SIZE.Height - System.Convert.ToSingle(96 * 0.75) - System.Convert.ToSingle(96 * 0.75));
		
		_parent = parent;
		_subName = subName;
	}
	
	public void AddHeader(string text)
	{
		
		int i;
		if (_header == null)
		{
			i = 0;
		}
		else
		{
			i = _header.Length;
		}
		Array.Resize(ref _header, i + 1);
		_header[i] = new Headers();
		_header[i].Text = text;
		
	}
	public void AddHeader(string text, int lineSkips, System.Windows.Media.FontFamily fontfamily, float fontsize, System.Windows.FontWeight fontweight, System.Windows.FontStyle fontstyle)
	{
		
		int i;
		if (_header == null)
		{
			i = 0;
		}
		else
		{
			i = _header.Length;
		}
		Array.Resize(ref _header, i + 1);
		_header[i] = new Headers(text, lineSkips, fontfamily, fontsize, fontweight, fontstyle);
		
	}
	
	private void setMaxHeight()
	{
		
		MAX_HEIGHT = System.Convert.ToSingle(PAGE_SIZE.Height - TOP_MARGIN - BOTTOM_MARGIN);
		
	}
	
	public void Display()
	{
		
		pg = 0;
		_currentHeight = 0;
		
		doc = new FixedDocument();
		doc.DocumentPaginator.PageSize = PAGE_SIZE;
		
		FixedPage fixedPage = new FixedPage();
		PageContent page = new PageContent();
		
		Type mType = _parent.GetType();
		System.Reflection.MethodInfo PrintMethod = mType.GetMethod(_subName);
		object[] obj = new object[] {this, fixedPage, page, doc};
		
		PrintMethod.Invoke(_parent, obj);
		
		DocumentViewer docViewer = new DocumentViewer();
		docViewer.Document = doc;
		frmReport frm = new frmReport();
		frm.DisplayGrid.Children.Add(docViewer);
		frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
		frm.Show();
		
	}
	
	public void processHeader(FixedPage page)
	{
		
		pg++;
		
		System.Windows.Controls.TextBlock thisText;
		_currentHeight = TOP_MARGIN;
		foreach (Headers txt in _header)
		{
			thisText = new System.Windows.Controls.TextBlock {Text = txt.TextBlock.Text, FontFamily = txt.TextBlock.FontFamily, FontSize = txt.TextBlock.FontSize};
			thisText.Text = thisText.Text.Replace("<p>", pg.ToString()).Replace("<P>", "Page " + pg.ToString());
			FixedPage.SetTop(thisText, _currentHeight);
			FixedPage.SetLeft(thisText, LEFT_MARGIN);
			FixedPage.SetRight(thisText, RIGHT_MARGIN);
			page.Children.Add((UIElement) thisText);
			_currentHeight += LINE_HEIGHT * txt.LineSkips;
		}
		
	}
	
	public System.Windows.Window Parent
	{
		get
		{
			return _parent;
		}
		set
		{
			_parent = value;
		}
	}
	public string PrintHandler
	{
		get
		{
			return _subName;
		}
		set
		{
			_subName = value.ToString();
		}
	}
	public int PageOrientation
	{
		get
		{
			return PAGE_ORIENTATION;
		}
		set
		{
			if (value >= 0 && value <= MAX_PAGE_ORIENTATION)
			{
				PAGE_ORIENTATION = System.Convert.ToInt16(value);
				PAGE_SIZE = PAGE_SIZES[PAPER_SIZE, PAGE_ORIENTATION];
				setMaxHeight();
			}
		}
	}
	public int PaperSize
	{
		get
		{
			return PAPER_SIZE;
		}
		set
		{
			if (value >= 0 && value <= MAX_PAPER_SIZE)
			{
				PAPER_SIZE = System.Convert.ToInt32(value);
				PAGE_SIZE = PAGE_SIZES[PAPER_SIZE, PAGE_ORIENTATION];
				setMaxHeight();
			}
		}
	}
	public System.Windows.Size PageSize
	{
		get
		{
			return PAGE_SIZE;
		}
		set
		{
			PAGE_SIZE = value;
			setMaxHeight();
		}
	}
	public float LineHeight
	{
		get
		{
			return LINE_HEIGHT;
		}
	}
	public float TopMargin
	{
		get
		{
			return TOP_MARGIN;
		}
		set
		{
			TOP_MARGIN = System.Convert.ToSingle(value);
			setMaxHeight();
		}
	}
	public float BottomMargin
	{
		get
		{
			return BOTTOM_MARGIN;
		}
		set
		{
			BOTTOM_MARGIN = System.Convert.ToSingle(value);
			setMaxHeight();
		}
	}
	public float LeftMargin
	{
		get
		{
			return LEFT_MARGIN;
		}
		set
		{
			LEFT_MARGIN = System.Convert.ToSingle(value);
		}
	}
	public float RightMargin
	{
		get
		{
			return RIGHT_MARGIN;
		}
		set
		{
			RIGHT_MARGIN = System.Convert.ToSingle(value);
		}
	}
	public float MaxHeight
	{
		get
		{
			return MAX_HEIGHT;
		}
	}
	public bool UseGroups
	{
		get
		{
			return _useGroups;
		}
		set
		{
			_useGroups = System.Convert.ToBoolean(value);
		}
	}
	public ArrayList Records
	{
		get
		{
			return _records;
		}
		set
		{
			_records = value;
			if (_records.Count > 0)
			{
				itmType = _records[0].GetType();
			}
		}
	}
	public Headers[] Header
	{
		get
		{
			return _header;
		}
		set
		{
			_header = value;
		}
	}
	public float CurrentHeight
	{
		get
		{
			return _currentHeight;
		}
		set
		{
			_currentHeight = System.Convert.ToSingle(value);
		}
	}
}

public class Headers
{
	
	private System.Windows.Controls.TextBlock _Text = new System.Windows.Controls.TextBlock();
	private int _lineSkips = 1;
	private float _lineHeight = System.Convert.ToSingle(0.167 * 96);
	
	public Headers()
	{
		
	}
	
	public Headers(string text, int lineSkips, System.Windows.Media.FontFamily fontfamily, float fontsize, System.Windows.FontWeight fontweight, System.Windows.FontStyle fontstyle)
	{
		
		_Text = new System.Windows.Controls.TextBlock {Text = text};
		_lineSkips = lineSkips;
		if (fontfamily != null)
		{
			_Text.FontFamily = fontfamily;
		}
		if (fontsize != null&& fontsize > 0)
		{
			_Text.FontSize = fontsize;
		}
		if (fontweight != null)
		{
			_Text.FontWeight = fontweight;
		}
		if (fontstyle != null)
		{
			_Text.FontStyle = fontstyle;
		}
		setLineHeight();
		
	}
	
	private void setLineHeight()
	{
		
		string text = "This is a test line";
		System.Drawing.Font font = new System.Drawing.Font(FontFamily.ToString(), 12, System.Drawing.FontStyle.Bold);
		Bitmap bitmap = new Bitmap(1, 1);
		Graphics g = Graphics.FromImage(bitmap);
		SizeF measureString = g.MeasureString(text, font);
		
		_lineHeight = System.Convert.ToSingle(measureString.Height + measureString.Height * 0.1);
		
	}
	
	public System.Windows.Controls.TextBlock TextBlock
	{
		get
		{
			return _Text;
		}
	}
	public string Text
	{
		get
		{
			return _Text.Text;
		}
		set
		{
			_Text.Text = value;
		}
	}
	public int LineSkips
	{
		get
		{
			return _lineSkips;
		}
		set
		{
			_lineSkips = System.Convert.ToInt32(value);
		}
	}
	public System.Windows.Media.FontFamily FontFamily
	{
		get
		{
			return _Text.FontFamily;
		}
		set
		{
			_Text.FontFamily = value;
		}
	}
	public System.Windows.FontWeight FontWeight
	{
		get
		{
			return _Text.FontWeight;
		}
		set
		{
			_Text.FontWeight = value;
		}
	}
	public System.Windows.FontStyle FontStyle
	{
		get
		{
			return _Text.FontStyle;
		}
		set
		{
			_Text.FontStyle = value;
			
		}
	}
	public float LineHeight
	{
		get
		{
			return _lineHeight;
		}
	}
}