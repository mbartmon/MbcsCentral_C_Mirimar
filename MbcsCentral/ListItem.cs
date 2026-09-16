namespace MbcsCentral
{
    public class ListItem
    {

        private string myDisplayItem;
        private long myValueItem;

        public ListItem() { }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Mike, 2017-03-14. </remarks>
        ///
        /// <param name="displayitem">  DisplayValuePath data. </param>
        /// <param name="valueitem">    Used to store a record key. </param>
        ///-------------------------------------------------------------------------------------------------

        public ListItem(string displayitem, long valueitem)
        {
            this.myDisplayItem = displayitem;
            this.myValueItem = valueitem;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Mike, 2017-03-14. </remarks>
        ///
        /// <param name="displayitem">  DisplayValuePath data. </param>
        /// <param name="valueitem">    Used to store a record key. </param>
        /// <param name="value">        The value. </param>
        ///-------------------------------------------------------------------------------------------------

        public ListItem(string displayitem, long valueitem, object value)
        {
            this.myDisplayItem = displayitem;
            this.myValueItem = valueitem;
            this.Value = value;
        }

        public string DisplayItem
        {
            get
            {
                return myDisplayItem;
            }
            set
            {
                myDisplayItem = value;
            }
        }
        public long ValueItem
        {
            get
            {
                return myValueItem;
            }
            set
            {
                myValueItem = value;
            }
        }
        public object Value {get; set;}

        public override string ToString()
        {
            return this.myDisplayItem;
        }
    }
}