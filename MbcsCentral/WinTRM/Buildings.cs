using Microsoft.VisualBasic;
using System.Data;
namespace WinTRM
{

	// $ID: Exp $
	public class Buildings : clsDB
	{


		protected const string TABLE = "Building";
		protected const string SQL_COL_ID = "Building Number";

		protected const string SQL_COL_ADDRESS_ONE = "Address 1";
		static string SQL_LOAD = "SELECT [" + SQL_COL_ID + "], [" + SQL_COL_ADDRESS_ONE + "] FROM " + TABLE + " WHERE [" + SQL_COL_ID + "]='?';";

		static string SQL_LOAD_ALL = "SELECT [" + SQL_COL_ID + "], [" + SQL_COL_ADDRESS_ONE + "] FROM " + TABLE + " ORDER BY [" + SQL_COL_ID + "];";
		static string SQL_INSERT = "place holder";

		static string SQL_UPDATE = "place holder";
		private string buildingNumber;

		private string address1;
		public Buildings() : base(TABLE, SQL_COL_ID, SQL_LOAD, SQL_LOAD_ALL, SQL_INSERT, SQL_UPDATE, GlobalShared.pmCN)
		{
		}

		private Buildings fill(DataRow dr)
		{

			this.buildingNumber = (System.String)dr(SQL_COL_ID);
			this.address1 = (System.String)dr(SQL_COL_ADDRESS_ONE);
			return this;

		}
		public static Buildings[] load()
		{

			ArrayList list = new ArrayList();
			DataSet ds = new DataSet();
			Buildings building = new Buildings();

			ds = building.loadAll();
			if ((ds != null)) {
				building = null;
				DataRow dr = default(DataRow);
				foreach ( dr in ds.Tables(0).Rows) {
					building = new Buildings();
					building.fill(dr);
					list.Add(building);
					building = null;
				}
				try {
					return (Buildings[])list.ToArray(typeof(Buildings));
				} catch {
					return null;
				}
			}
			return null;

		}
		public static Buildings load(string id)
		{

			Buildings building = new Buildings();
			DataSet ds = new DataSet();
			DataRow dr = null;
			ds = building.loadByPrimaryKey(id);
			dr = ds.Tables(0).Rows(0);
			return building.fill(dr);

		}

		public string getBuildingNumber()
		{
			return buildingNumber;
		}
		public string getAddressOne()
		{
			return address1;
		}
	}
}
