namespace MbcsCentral.WinTrm
{
	public partial class WaitWindow
	{

		//Dim t As New Timers.Timer
		int c;

		int m = 5;

		private void Animate()
		{
			txtWait.Content = "Please wait";
		}


		private void WaitWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//AddHandler t.Elapsed, AddressOf Animate
			//Animate()
			//t.Interval = 1000
			//t.Start()

			txtWait.Focus();


		}

		private void WaitWindow_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//t.Stop()
		}
		public WaitWindow()
		{
            InitializeComponent();
			Unloaded += WaitWindow_Unloaded;
			Loaded += WaitWindow_Loaded;
		}
	}
}
