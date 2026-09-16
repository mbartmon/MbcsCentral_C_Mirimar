using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MbcsCentral.WinGL
{
    public class MajorCategoryList
    {

        private List<MajorCategories> major;

        public MajorCategoryList() 
        { 
            major = new List<MajorCategories>();
        }

        public List<MajorCategories> Major { get; set; } 

        public List<MajorCategories> GetCategories()
        {
            Category[] cats = Category.loadBySubKey("0_");
            if (cats != null && cats.Count() > 0)
            {
                foreach (Category cat in cats)
                {
                    MajorCategories mcat = new MajorCategories(cat);
                    SubCat[] subs = FillSubs(cat.Key);
                    if (subs != null && subs.Count() > 0)
                    {
                        foreach(SubCat scat in subs)
                        {
                            scat.Parent = mcat;
                            mcat.SubCats.Add(scat);
                        }
                    }
                    major.Add(mcat);
                }
                return major;
            }
            return null;
        }

        static private SubCat[] FillSubs(string subKey)
        {
            SubCat[] cats = SubCat.BySubKey(subKey);
            if (cats != null && cats.Count() > 0)
            {
                ArrayList subList = new ArrayList();
                foreach (SubCat cat in cats)
                {
                    SubSubCat[] subSubcats = SubSubCat.BySubSubKey(cat.Key);
                    if (subSubcats != null && subSubcats.Count() > 0)
                    {
                        foreach (SubSubCat subsubcat in subSubcats)
                        {
                            subsubcat.Parent = cat;
                            cat.SubSubCats.Add(subsubcat);
                        }
                    }
                    subList.Add(cat);
                }
                try
                {
                    return (SubCat[])subList.ToArray(typeof(SubCat));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static short GetHighestSequence(List<MajorCategories> mList)
        {
            short seq = 0;
            foreach (MajorCategories cat in mList)
            {
                if (cat.Sequence > seq)
                    seq = cat.Sequence;
                if (cat.SubCats != null)
                {
                    foreach (SubCat scat in cat.SubCats)
                    {
                        if (scat.Sequence > seq)
                            seq = scat.Sequence; ;
                        if (scat.SubSubCats != null)
                        {
                            foreach (SubSubCat sscat in scat.SubSubCats)
                            {
                                if (sscat.Sequence > seq)
                                    seq = sscat.Sequence;
                            }
                        }
                    }
                }
            }
            return seq;
        }

        public static string GetHighestKey(List<MajorCategories> mList)
        {
            string key = "";
            string compKey = "";
            int c;
            foreach (MajorCategories cat in mList)
            {
                compKey = cat.Key;
                c = string.Compare(compKey, key);
                if (c > 0)
                    key = compKey;
                if (cat.SubCats != null)
                {
                    foreach (SubCat scat in cat.SubCats)
                    {
                        compKey = scat.Key;
                        c = string.Compare(compKey, key);
                        if (c > 0)
                            key = compKey;
                        if (scat.SubSubCats != null)
                        {
                            foreach (SubSubCat sscat in scat.SubSubCats)
                            {
                                compKey = sscat.Key;
                                c = string.Compare(compKey, key);
                                if (c > 0)
                                    key = compKey;
                            }
                        }
                    }
                }
            }
            return key;
        }

        public static void RenumberCategories(ref List<MajorCategories> mlist)
        {
            short seq = 0;
            foreach(MajorCategories cat in mlist)
            {
                cat.Sequence = ++seq;
                if (cat.SubCats != null)
                {
                    foreach (SubCat scat in cat.SubCats)
                    {
                        scat.Sequence = ++seq;
                        if (scat.SubSubCats != null)
                        {
                            foreach(SubSubCat sscat in scat.SubSubCats)
                            {
                                sscat.Sequence = ++seq;
                            }
                        }
                    }
                }
            }
        }
    }

    public class MajorCategories : Category
    {
        public MajorCategories() 
        {
            this.CatType = (int)Category.CatTypes.Major;
        }

        public MajorCategories(Category cat)
        {
            m_Sequence = cat.Sequence;
            m_Id = cat.Id;
            m_Key = cat.Key;
            m_SubOf = cat.SubOf;
            m_CategoryDescription = cat.CategoryDescription;
            m_OldKey = cat.OldKey;
            this.CatType = (int)Category.CatTypes.Major;
            this.SubCats = new ObservableCollection<SubCat>();
        }

        public SubCat DemoteToSub(bool keep)
        {
            SubCat newCat = new SubCat(this); // Make this MajorCategory a SubCategory
            if (this.SubCats != null)
            {
                foreach (SubCat scat in this.SubCats)
                {
                    SubSubCat newSscat = new SubSubCat(scat);
                    newSscat.Parent = newCat;
                    newCat.SubSubCats.Add(newSscat);
                }
                if (keep)
                {
                    foreach (SubCat scat in this.SubCats)
                    {
                        if (scat.SubSubCats != null)
                        {
                            foreach (SubSubCat sscat in scat.SubSubCats)
                            {
                                newCat.SubSubCats.Add(sscat);
                            }
                        }
                    }
                }

            }
            return newCat;
        }

        public ObservableCollection<SubCat> SubCats { get; set; }
    }

    public class SubCat : Category
    {
        private SubSubCat[] subSubCats;
        public SubCat() 
        {
            this.CatType = (int)Category.CatTypes.Sub;
            this.SubSubCats = new ObservableCollection<SubSubCat>();
        }
        
        public SubCat(Category cat)
        {
            m_Sequence = cat.Sequence;
            m_Id = cat.Id;
            m_Key = cat.Key;
            m_SubOf = cat.SubOf;
            m_CategoryDescription = cat.CategoryDescription;
            m_OldKey = cat.OldKey;
            this.Parent = cat.Parent;
            this.CatType = (int)Category.CatTypes.Sub;
            this.SubSubCats = new ObservableCollection<SubSubCat>();

        }
        public MajorCategories PromoteToMajor()
        {
            MajorCategories cat = new MajorCategories(this);
            if (cat.SubCats != null)
            {
                foreach (SubSubCat sscat in this.SubSubCats)
                {
                    cat.SubCats.Add(new SubCat(sscat));
                }
            }
            return cat;
        }

        public SubSubCat DemoteToSubSub()
        {
            SubSubCat newCat = null;
            return newCat;
        }

        public static SubCat[] BySubKey(string subKey, bool except = false)
        {
            ArrayList list = new ArrayList();
            string[] opt = { subKey };
            DataSet ds = new DataSet();
            SubCat obj = new SubCat();

            if (except)
                compare = " <> '?'";
            else
                compare = " = '?'";
            SQL_LOAD_BY_SUB_KEY = SQL_LOAD_BY_SUB_KEY.Replace("<compare>", compare);
            ds = obj.load(SQL_LOAD_BY_SUB_KEY, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new SubCat();
                obj.fill(dr);
                obj.CatType = (int)Category.CatTypes.Sub;
                list.Add(obj);
                obj = null;
            }
            try
            {
                return (SubCat[])list.ToArray(typeof(SubCat));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ObservableCollection<SubSubCat> SubSubCats { get; set; }

    }

    public class SubSubCat : Category
    {
        public SubSubCat() 
        {
            this.CatType = (int)Category.CatTypes.SubSub;
        }

        public SubSubCat(Category cat)
        {
            m_Sequence = cat.Sequence;
            m_Id = cat.Id;
            m_Key = cat.Key;
            m_SubOf = cat.SubOf;
            m_CategoryDescription = cat.CategoryDescription;
            m_OldKey = cat.OldKey;
            this.Parent = cat.Parent;
        }

        public static SubSubCat[] BySubSubKey(string subKey, bool except = false)
        {
            ArrayList list = new ArrayList();
            string[] opt = { subKey };
            DataSet ds = new DataSet();
            SubSubCat obj = new SubSubCat();

            if (except)
                compare = " <> '?'";
            else
                compare = " = '?'";
            SQL_LOAD_BY_SUB_KEY = SQL_LOAD_BY_SUB_KEY.Replace("<compare>", compare);
            ds = obj.load(SQL_LOAD_BY_SUB_KEY, opt);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                return null;
            obj = null;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                obj = new SubSubCat();
                obj.fill(dr);
                obj.CatType = (int)Category.CatTypes.SubSub;
                list.Add(obj);
                obj = null;
            }
            try 
	        {	        
                return (SubSubCat[])list.ToArray(typeof(SubSubCat));
		
	        }
	        catch (Exception ex)
	        {
                return null;
		
	        }
        }
    }
}
