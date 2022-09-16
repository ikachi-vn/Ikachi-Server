//------------------------------------------------------------------------------
// 
//    www.codeart.vn
//    hungvq@live.com
//    (+84)908.061.119
// 
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    
    using System;
    using System.Collections.Generic;
    
    
    public partial class tbl_BI_Operating_MarketResearch
    {
        public int IDAccount { get; set; }
        public int IDService { get; set; }
        public Nullable<int> IDProductType { get; set; }
        public int Id { get; set; }
        public int Frequency { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int NumberOfEvent { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal Revenue { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}


namespace DTOModel
{
	using System;
	public partial class DTO_BI_Operating_MarketResearch
	{
		public int IDAccount { get; set; }
		public int IDService { get; set; }
		public Nullable<int> IDProductType { get; set; }
		public int Id { get; set; }
		public int Frequency { get; set; }
		public int Day { get; set; }
		public int Month { get; set; }
		public int Quarter { get; set; }
		public int Year { get; set; }
		public int NumberOfEvent { get; set; }
		public int NumberOfGuests { get; set; }
		public decimal Revenue { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime ModifiedDate { get; set; }
	}
}


namespace BaseBusiness
{
    using ClassLibrary;
    using DTOModel;
    using Newtonsoft.Json.Linq;
    using OfficeOpenXml;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;

    public static partial class BS_BI_Operating_MarketResearch 
    {
        public static dynamic getSearch(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch,StaffID, QueryStrings);
        }

		public static IQueryable<DTO_BI_Operating_MarketResearch> get(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
			var query = _queryBuilder(db, IDBranch, StaffID, QueryStrings);
            query = _sortBuilder(query, QueryStrings);
            query = _pagingBuilder(query, QueryStrings);

			return toDTO(query);
        }

        public static List<ItemModel> getValidateData(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch, StaffID, QueryStrings).Select(s => new ItemModel { 
                Id = s.Id, }).ToList();
        }

        public static string export(ExcelPackage package, AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            package.Workbook.Properties.Title = "ART-DMS BI_Operating_MarketResearch";
            package.Workbook.Properties.Author = "hung.vu@codeart.vn";
            package.Workbook.Properties.Application = "ART-DMS";
            package.Workbook.Properties.Company = "A.R.T Distribution";

            ExcelWorkbook workBook = package.Workbook;
            if (workBook != null)
            {
                var ws = workBook.Worksheets.FirstOrDefault();
                var data = _queryBuilder(db, IDBranch, StaffID, QueryStrings).ToList();

                 

                int SheetColumnsCount, SheetRowCount = 0;
                SheetColumnsCount = ws.Dimension.End.Column;    // Find End Column
                SheetRowCount = ws.Dimension.End.Row;           // Find End Row

                int rowid = 5;
                int firstColInex = 0;

                #region readPropertyList
                var propList = new List<Tuple<string, string, string, List<ItemModel>>>()
                    .Select(t => new { ClassName = t.Item1, PropertyName = t.Item2, ReffProperty = t.Item3, ValidateData = t.Item4 }).ToList();

                for (int i = 1; i <= SheetColumnsCount; i++)
                {
                    string p = ws.Cells[1, i].Value == null ? "" : ws.Cells[1, i].Text;
                    string modalPart = p.Split('|')[0];
                    string queryPart = "";
                    string className = "";
                    string reffProperty = "Id";

                    if (p.Contains("|")) //Sample property BRA_Branch?Select=Code&IDType=115|IDBranch
                        if (modalPart.Contains("?"))
                        {
                            className = modalPart.Split('?')[0];
                            queryPart = modalPart.Split('?')[1];
                            if (queryPart.Contains("&"))
                            {
                                string[] query = queryPart.Split('&');
                                foreach (var q in query)
                                {
                                    string key = "";
                                    string value = "";

                                    if (q.Contains("="))
                                    {
                                        key = q.Split('=')[0];
                                        value = q.Split('=')[1];
                                    }
                                    else
                                        key = q;

                                    if (QueryStrings.ContainsKey(key))
                                        QueryStrings.Remove(key);

                                    QueryStrings.Add(key, value);
                                }
                            }
                        }
                        else
                            className = modalPart;
                    

                    List<ItemModel> vdata = null;
                    if (className != "")
                    {
                        Type type = Type.GetType("BaseBusiness.BS_" + className + ", ClassLibrary");
                        System.Reflection.MethodInfo dynamicGet = type == null ? null : type.GetMethod("getValidateData");
                        if (dynamicGet != null)
                            vdata = (List<ItemModel>)dynamicGet.Invoke(null, new object[] { db, IDBranch, StaffID, QueryStrings });
                        ExcelUtil.SetValidateData(package, i, vdata);
                    }

                    propList.Add(new
                    {
                        ClassName = className,
                        PropertyName = (p.Contains("|") ? p.Split('|')[1] : p),
                        ReffProperty = reffProperty,
                        ValidateData = vdata
                    });
                }
                #endregion

                foreach (var item in data)
                {
                    for (int i = 1; i <= SheetColumnsCount; i++)
                    {
                        var property = propList[i - 1];
                        if (!string.IsNullOrEmpty(property.PropertyName) && item.GetType().GetProperties().Any(d => d.Name == property.PropertyName))
                        {
                            if (firstColInex == 0)
                                firstColInex = i;

                            if (property.ClassName != "")
                            {
                                ItemModel it = null;
                                if (property.ReffProperty == "Id")
                                {
                                    var val = item.GetType().GetProperties().First(o => o.Name == property.PropertyName).GetValue(item, null);
                                    if (val != null && property.ValidateData != null)
                                    {
                                        int.TryParse(val.ToString(), out int id);
                                        if (id > 0)
                                            it = property.ValidateData.FirstOrDefault(d => d.Id == id);
                                    }
                                }
                                else if (property.ReffProperty == "Code")
                                {
                                    string code = (string)item.GetType().GetProperties().First(o => o.Name == property.PropertyName).GetValue(item, null);
                                    if (!string.IsNullOrEmpty(code))
                                        it = property.ValidateData.FirstOrDefault(d => d.Code == code);
                                }

                                if (it != null)
                                    ws.Cells[rowid, i].Value = (property.ReffProperty == "Id"? it.Id.ToString() : it.Code) + ". " + it.Name;
                                
                            }
                            else
                                ws.Cells[rowid, i].Value = item.GetType().GetProperties().First(o => o.Name == property.PropertyName).GetValue(item, null);
                        }
                    }
                    rowid++;
                }

                //create a range for the table
                var range = ws.Cells[4, firstColInex, rowid-1, SheetColumnsCount];
                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.FromArgb(235,235,235));
                range.Style.Border.DiagonalDown = true;

                range.AutoFilter = true;
                //ws.Cells.AutoFilter = true;
                //ws.Cells.AutoFitColumns(6, 60);

                package.Save();
            }

            return package.File.FullName.Substring(package.File.FullName.IndexOf("\\Uploads\\"));
        }

		public static DTO_BI_Operating_MarketResearch getAnItem(AppEntities db, int IDBranch, int StaffID, int id)
        {
            var dbResult = db.tbl_BI_Operating_MarketResearch.Find(id);

			return toDTO(dbResult);
			
        }
		

		public static bool put(AppEntities db, int IDBranch, int StaffID, int Id, dynamic item, string Username, Dictionary<string, string> QueryStrings)
        {
            bool result = false;
            var dbitem = db.tbl_BI_Operating_MarketResearch.Find(Id);
            
            if (dbitem != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                try
                {
                    db.SaveChanges();
					result = true;
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("put_BI_Operating_MarketResearch",e);
                    result = false;
                }
            }
            else
                if (QueryStrings.Any(d => d.Key == "ForceCreate"))
                    result =  post(db, IDBranch, StaffID, item, Username) != null;
                
            return result;
        }

        public static void patchDynamicToDB(dynamic item, tbl_BI_Operating_MarketResearch dbitem, string Username)
        {
            Type type = typeof(tbl_BI_Operating_MarketResearch);
            List<string> Props = new List<string>();
            try
            {
                if (item.GetType().Name == "JObject")
                    foreach (JProperty prop in item.Properties())
                        Props.Add(prop.Name);
                else
                    foreach (var prop in item.GetType().GetProperties())
                        Props.Add(prop.Name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            foreach (string prop in Props)
            {
                if ("|CreatedBy|CreatedDate|ModifiedBy|ModifiedDate|IsDisabled|IsDeleted|".Contains("|" + prop + "|"))
                    continue;

                var tprop = type.GetProperty(prop);
                if (tprop != null)
                {
                    var value = item.GetType().Name == "JObject" ? item[prop].Value : item.GetType().GetProperty(prop).GetValue(item, null);
                    if (prop == "Id" && string.IsNullOrEmpty(Convert.ToString(value))) value = 0;
                    var safeValue = (value == null) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(tprop.PropertyType) ?? tprop.PropertyType);
                    tprop.SetValue(dbitem, safeValue);
                }
            }

            dbitem.ModifiedBy = Username;
			dbitem.ModifiedDate = DateTime.Now;
        }

        public static void patchDTOtoDBValue( DTO_BI_Operating_MarketResearch item, tbl_BI_Operating_MarketResearch dbitem)
        {
            if (item == null){
                dbitem = null;
                return;
            }
            							
			dbitem.IDAccount = item.IDAccount;							
			dbitem.IDService = item.IDService;							
			dbitem.IDProductType = item.IDProductType;							
			dbitem.Frequency = item.Frequency;							
			dbitem.Day = item.Day;							
			dbitem.Month = item.Month;							
			dbitem.Quarter = item.Quarter;							
			dbitem.Year = item.Year;							
			dbitem.NumberOfEvent = item.NumberOfEvent;							
			dbitem.NumberOfGuests = item.NumberOfGuests;							
			dbitem.Revenue = item.Revenue;							
			dbitem.IsDeleted = item.IsDeleted;        }

		public static DTO_BI_Operating_MarketResearch post(AppEntities db, int IDBranch, int StaffID, dynamic item, string Username)
        {
            tbl_BI_Operating_MarketResearch dbitem = new tbl_BI_Operating_MarketResearch();
            if (item != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                
				dbitem.CreatedBy = Username;
				dbitem.CreatedDate = DateTime.Now;				
                try
                {
					db.tbl_BI_Operating_MarketResearch.Add(dbitem);
                    db.SaveChanges();
				
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("post_BI_Operating_MarketResearch",e);
                    return null;
                }
            }
            return toDTO(dbitem);
        }

        public static dynamic import(ExcelPackage package, AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings, string Username)
        {
            ExcelWorkbook workBook = package.Workbook;
            int errCount = 0;
            var errList = new List<Tuple<int, int, string>>().Select(t => new { Id = t.Item1, Line = t.Item2, Message = t.Item3 }).ToList();
            
            if (workBook != null)
            {
                Type type = Type.GetType("BaseBusiness.BS_BI_Operating_MarketResearch, ClassLibrary");
                var ws = workBook.Worksheets.FirstOrDefault();
                int SheetColumnsCount = ws.Dimension.End.Column;
                int SheetRowCount = ws.Dimension.End.Row;
                int firstRow = 5;
                int firstColIndex = 1;
                
                for (int r = firstRow; r <= SheetRowCount; r++){
                    dynamic it = new Newtonsoft.Json.Linq.JObject();
                    bool isBreak = false;
                    for (int c = firstColIndex; c <= SheetColumnsCount; c++)
                    {
                        dynamic convert = new Newtonsoft.Json.Linq.JObject();
                        var target = new tbl_BI_Operating_MarketResearch();
                        string property = ws.Cells[1, c].Value == null ? "" : ws.Cells[1, c].Text;
                        if(!string.IsNullOrEmpty(property))
                        {
                            var v = ws.Cells[r, c].Value == null ? "" : ws.Cells[r, c].Value.ToString();
                            try
                            {
                                if (property.Contains("|"))
                                {
                                    it[property] = v;
                                    property = property.Split('|')[1];
                                    if (v.Contains("."))
                                        v = v.Split('.')[0];
                                }

                                convert[property] = string.IsNullOrEmpty(v) ? null : v;
                                patchDynamicToDB(convert, target, Username);
                                System.Reflection.MethodInfo validate = type.GetMethod("validate");
                                if (validate != null) {
                                    string message = (string)validate.Invoke(null, new object[] { target, property, db, IDBranch, StaffID, QueryStrings, Username });

                                    if (message != "OK")
                                    {
                                        errCount++;
                                        errList.Add(new { Id = errCount, Line = r, Message = message });
                                        ExcelUtil.NoteToWorkSheet(ws, r, c, message, System.Drawing.Color.Red, System.Drawing.Color.White);
                                        isBreak = true;
                                        continue;
                                    }
                                }
                                it[property] = convert[property];
                            }
                            catch (Exception ex)
                            {
                                errCount++;
                                errList.Add(new { Id = errCount, Line = r, Message = ex.Message });
                                ExcelUtil.NoteToWorkSheet(ws, r, c, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
                                isBreak = true;
                                continue;
                            }
                        }
                    }
                    if (isBreak) continue;
                    
                    tbl_BI_Operating_MarketResearch dbitem = new tbl_BI_Operating_MarketResearch();
                    if (it.Id == null || string.IsNullOrEmpty(it.Id.Value) || it.Id == "0")
                    {
                        dbitem = new tbl_BI_Operating_MarketResearch();
                        dbitem.CreatedBy = Username;
				        dbitem.CreatedDate = DateTime.Now;
                                            }
                    else
                    {
                        dbitem = db.tbl_BI_Operating_MarketResearch.Find((int)it.Id);
                    }

                    if (dbitem == null)
                    {
                        errCount++;
                        string message = "Không tìm được dữ liệu (Id: " + it.Id + ")";
                        errList.Add(new { Id = errCount, Line = r, Message = message });
                        ExcelUtil.NoteToWorkSheet(ws, r, firstColIndex, message, System.Drawing.Color.Red, System.Drawing.Color.White);
                        continue;
                    }
                    try
                    {
                        patchDynamicToDB(it, dbitem, Username);
                        System.Reflection.MethodInfo fillReference = type.GetMethod("fillReference");
                        if (fillReference != null)
                            fillReference.Invoke(null, new object[] { dbitem, it, db, IDBranch, StaffID, QueryStrings, Username });
                        
                        if (dbitem.Id == 0) db.tbl_BI_Operating_MarketResearch.Add(dbitem);
                    }
                    catch (Exception ex)
                    {
                        errCount++;
                        errList.Add(new { Id = errCount, Line = r, Message = ex.Message });
                        ExcelUtil.NoteToWorkSheet(ws, r, firstColIndex, ex.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
                        continue;
                    }
                    
                    
                  
                }

                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    errCount++;
                    errList.Add(new { Id = errCount, Line = firstRow, Message = e.Message });
                    ExcelUtil.NoteToWorkSheet(ws, firstRow, firstColIndex, e.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
                }
                catch (DbEntityValidationException e)
                {
                    errorLog.logMessage("post_BI_Operating_MarketResearch",e);
                }
                catch (Exception e)
                {
                    errCount++;
                    errList.Add(new { Id = errCount, Line = firstRow, Message = e.Message });
                    ExcelUtil.NoteToWorkSheet(ws, firstRow, firstColIndex, e.Message, System.Drawing.Color.Red, System.Drawing.Color.White);
                }

            }
            if (errCount > 0)
            {
                package.Save();
            }

            return new
            {
                ErrorList = errList,
                FileUrl = package.File.FullName.Substring(package.File.FullName.IndexOf("\\Uploads\\")).Replace("\\", "/")
            };
        }

		public static bool delete(AppEntities db, string ids, string Username)
        {
			bool result = false;

            var IDList = ids.Replace("[", "").Replace("]", "").Split(',');
            List<int?> IDs = new List<int?>();
            foreach (var item in IDList)
                if (int.TryParse(item, out int i))
                    IDs.Add(i);
                else if (item == "null")
                    IDs.Add(null);
            if (IDs.Count == 0){
                return result;
            }

            var dbitems = db.tbl_BI_Operating_MarketResearch.Where(d => IDs.Contains(d.Id));
            var updateDate = DateTime.Now;
            List<int?> IDBranches = new List<int?>();

            foreach (var dbitem in dbitems)
            {
							
				dbitem.ModifiedBy = Username;
				dbitem.ModifiedDate = updateDate;
				dbitem.IsDeleted = true;
							            
                
            }

            try
            {
                db.SaveChanges();
                result = true;
			
                BS_Version.update_Version(db, null, "DTO_BI_Operating_MarketResearch", updateDate, Username);
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("delete_BI_Operating_MarketResearch",e);
                result = false;
            }

            return result;
        }

        public static bool disable(AppEntities db, string ids, bool isDisable, string Username)
        {
            bool result = false;
            
            return result;
        }

		
		//---//
		public static bool check_Exists(AppEntities db, int id)
		{
             
            return db.tbl_BI_Operating_MarketResearch.Any(e => e.Id == id && e.IsDeleted == false);
            
		}
		
		//---//
		public static IQueryable<DTO_BI_Operating_MarketResearch> toDTO(IQueryable<tbl_BI_Operating_MarketResearch> query)
        {
			return query
			.Select(s => new DTO_BI_Operating_MarketResearch(){							
				IDAccount = s.IDAccount,							
				IDService = s.IDService,							
				IDProductType = s.IDProductType,							
				Id = s.Id,							
				Frequency = s.Frequency,							
				Day = s.Day,							
				Month = s.Month,							
				Quarter = s.Quarter,							
				Year = s.Year,							
				NumberOfEvent = s.NumberOfEvent,							
				NumberOfGuests = s.NumberOfGuests,							
				Revenue = s.Revenue,							
				IsDeleted = s.IsDeleted,							
				CreatedBy = s.CreatedBy,							
				ModifiedBy = s.ModifiedBy,							
				CreatedDate = s.CreatedDate,							
				ModifiedDate = s.ModifiedDate,					
			});//;
                              
        }

		public static DTO_BI_Operating_MarketResearch toDTO(tbl_BI_Operating_MarketResearch dbResult)
        {
			if (dbResult != null)
			{
				return new DTO_BI_Operating_MarketResearch()
				{							
					IDAccount = dbResult.IDAccount,							
					IDService = dbResult.IDService,							
					IDProductType = dbResult.IDProductType,							
					Id = dbResult.Id,							
					Frequency = dbResult.Frequency,							
					Day = dbResult.Day,							
					Month = dbResult.Month,							
					Quarter = dbResult.Quarter,							
					Year = dbResult.Year,							
					NumberOfEvent = dbResult.NumberOfEvent,							
					NumberOfGuests = dbResult.NumberOfGuests,							
					Revenue = dbResult.Revenue,							
					IsDeleted = dbResult.IsDeleted,							
					CreatedBy = dbResult.CreatedBy,							
					ModifiedBy = dbResult.ModifiedBy,							
					CreatedDate = dbResult.CreatedDate,							
					ModifiedDate = dbResult.ModifiedDate,
				};
			}
			else
				return null; 
        }

		public static IQueryable<tbl_BI_Operating_MarketResearch> _queryBuilder(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            
			IQueryable<tbl_BI_Operating_MarketResearch> query = db.tbl_BI_Operating_MarketResearch.AsNoTracking();//.Where(d => d.IsDeleted == false );
			

			//Query keyword



			//Query IDAccount (int)
			if (QueryStrings.Any(d => d.Key == "IDAccount"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDAccount").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int> IDs = new List<int>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDAccount));
            }

			//Query IDService (int)
			if (QueryStrings.Any(d => d.Key == "IDService"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDService").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int> IDs = new List<int>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDService));
            }

			//Query IDProductType (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDProductType"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDProductType").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDProductType));
////                    query = query.Where(d => IDs.Contains(d.IDProductType));
//                    
            }

			//Query Id (int)
			if (QueryStrings.Any(d => d.Key == "Id"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "Id").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int> IDs = new List<int>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.Id));
            }

			//Query Frequency (int)
			if (QueryStrings.Any(d => d.Key == "FrequencyFrom") && QueryStrings.Any(d => d.Key == "FrequencyTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "FrequencyFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "FrequencyTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Frequency && d.Frequency <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Frequency"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Frequency").Value, out int val))
                    query = query.Where(d => val == d.Frequency);


			//Query Day (int)
			if (QueryStrings.Any(d => d.Key == "DayFrom") && QueryStrings.Any(d => d.Key == "DayTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DayFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DayTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Day && d.Day <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Day"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Day").Value, out int val))
                    query = query.Where(d => val == d.Day);


			//Query Month (int)
			if (QueryStrings.Any(d => d.Key == "MonthFrom") && QueryStrings.Any(d => d.Key == "MonthTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "MonthFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "MonthTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Month && d.Month <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Month"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Month").Value, out int val))
                    query = query.Where(d => val == d.Month);


			//Query Quarter (int)
			if (QueryStrings.Any(d => d.Key == "QuarterFrom") && QueryStrings.Any(d => d.Key == "QuarterTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "QuarterFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "QuarterTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Quarter && d.Quarter <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Quarter"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Quarter").Value, out int val))
                    query = query.Where(d => val == d.Quarter);


			//Query Year (int)
			if (QueryStrings.Any(d => d.Key == "YearFrom") && QueryStrings.Any(d => d.Key == "YearTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "YearFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "YearTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Year && d.Year <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Year"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Year").Value, out int val))
                    query = query.Where(d => val == d.Year);


			//Query NumberOfEvent (int)
			if (QueryStrings.Any(d => d.Key == "NumberOfEventFrom") && QueryStrings.Any(d => d.Key == "NumberOfEventTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfEventFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfEventTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.NumberOfEvent && d.NumberOfEvent <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "NumberOfEvent"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfEvent").Value, out int val))
                    query = query.Where(d => val == d.NumberOfEvent);


			//Query NumberOfGuests (int)
			if (QueryStrings.Any(d => d.Key == "NumberOfGuestsFrom") && QueryStrings.Any(d => d.Key == "NumberOfGuestsTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuestsFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuestsTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.NumberOfGuests && d.NumberOfGuests <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "NumberOfGuests"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuests").Value, out int val))
                    query = query.Where(d => val == d.NumberOfGuests);


			//Query Revenue (decimal)
			if (QueryStrings.Any(d => d.Key == "RevenueFrom") && QueryStrings.Any(d => d.Key == "RevenueTo"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "RevenueFrom").Value, out decimal fromVal) && decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "RevenueTo").Value, out decimal toVal))
                    query = query.Where(d => fromVal <= d.Revenue && d.Revenue <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Revenue"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Revenue").Value, out decimal val))
                    query = query.Where(d => val == d.Revenue);


			//Query IsDeleted (bool)
			if (QueryStrings.Any(d => d.Key == "IsDeleted"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "IsDeleted").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.IsDeleted);
            }
            else
                query = query.Where(d => false == d.IsDeleted);

			//Query CreatedBy (string)
			if (QueryStrings.Any(d => d.Key == "CreatedBy_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "CreatedBy_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "CreatedBy_eq").Value;
                query = query.Where(d=>d.CreatedBy == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "CreatedBy") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "CreatedBy").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "CreatedBy").Value;
                query = query.Where(d=>d.CreatedBy.Contains(keyword));
            }
            

			//Query ModifiedBy (string)
			if (QueryStrings.Any(d => d.Key == "ModifiedBy_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ModifiedBy_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ModifiedBy_eq").Value;
                query = query.Where(d=>d.ModifiedBy == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "ModifiedBy") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ModifiedBy").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ModifiedBy").Value;
                query = query.Where(d=>d.ModifiedBy.Contains(keyword));
            }
            

			//Query CreatedDate (System.DateTime)
			if (QueryStrings.Any(d => d.Key == "CreatedDateFrom") && QueryStrings.Any(d => d.Key == "CreatedDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "CreatedDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "CreatedDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.CreatedDate && d.CreatedDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "CreatedDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "CreatedDate").Value, out DateTime val))
                    query = query.Where(d => d.CreatedDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.CreatedDate));


			//Query ModifiedDate (System.DateTime)
			if (QueryStrings.Any(d => d.Key == "ModifiedDateFrom") && QueryStrings.Any(d => d.Key == "ModifiedDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ModifiedDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ModifiedDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.ModifiedDate && d.ModifiedDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "ModifiedDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ModifiedDate").Value, out DateTime val))
                    query = query.Where(d => d.ModifiedDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.ModifiedDate));

            return query;
        }
		
        public static IQueryable<tbl_BI_Operating_MarketResearch> _sortBuilder(IQueryable<tbl_BI_Operating_MarketResearch> query, Dictionary<string, string> QueryStrings)
        {
            if (QueryStrings.Any(d => d.Key == "SortBy"))
            {
                var sorts = QueryStrings.FirstOrDefault(d => d.Key == "SortBy").Value.Replace("[", "").Replace("]", "").Split(',');
                
                foreach (var item in sorts)
                    if (!string.IsNullOrEmpty(item))
                    {
                        var ordered = query as IOrderedQueryable<tbl_BI_Operating_MarketResearch>;
                        bool isOrdered = ordered.ToString().IndexOf("ORDER BY") !=-1;

                        switch (item)
                        {
							case "IDAccount":
								query = isOrdered ? ordered.ThenBy(o => o.IDAccount) : query.OrderBy(o => o.IDAccount);
								 break;
							case "IDAccount_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDAccount) : query.OrderByDescending(o => o.IDAccount);
                                break;
							case "IDService":
								query = isOrdered ? ordered.ThenBy(o => o.IDService) : query.OrderBy(o => o.IDService);
								 break;
							case "IDService_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDService) : query.OrderByDescending(o => o.IDService);
                                break;
							case "IDProductType":
								query = isOrdered ? ordered.ThenBy(o => o.IDProductType) : query.OrderBy(o => o.IDProductType);
								 break;
							case "IDProductType_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDProductType) : query.OrderByDescending(o => o.IDProductType);
                                break;
							case "Id":
								query = isOrdered ? ordered.ThenBy(o => o.Id) : query.OrderBy(o => o.Id);
								 break;
							case "Id_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Id) : query.OrderByDescending(o => o.Id);
                                break;
							case "Frequency":
								query = isOrdered ? ordered.ThenBy(o => o.Frequency) : query.OrderBy(o => o.Frequency);
								 break;
							case "Frequency_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Frequency) : query.OrderByDescending(o => o.Frequency);
                                break;
							case "Day":
								query = isOrdered ? ordered.ThenBy(o => o.Day) : query.OrderBy(o => o.Day);
								 break;
							case "Day_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Day) : query.OrderByDescending(o => o.Day);
                                break;
							case "Month":
								query = isOrdered ? ordered.ThenBy(o => o.Month) : query.OrderBy(o => o.Month);
								 break;
							case "Month_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Month) : query.OrderByDescending(o => o.Month);
                                break;
							case "Quarter":
								query = isOrdered ? ordered.ThenBy(o => o.Quarter) : query.OrderBy(o => o.Quarter);
								 break;
							case "Quarter_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Quarter) : query.OrderByDescending(o => o.Quarter);
                                break;
							case "Year":
								query = isOrdered ? ordered.ThenBy(o => o.Year) : query.OrderBy(o => o.Year);
								 break;
							case "Year_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Year) : query.OrderByDescending(o => o.Year);
                                break;
							case "NumberOfEvent":
								query = isOrdered ? ordered.ThenBy(o => o.NumberOfEvent) : query.OrderBy(o => o.NumberOfEvent);
								 break;
							case "NumberOfEvent_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.NumberOfEvent) : query.OrderByDescending(o => o.NumberOfEvent);
                                break;
							case "NumberOfGuests":
								query = isOrdered ? ordered.ThenBy(o => o.NumberOfGuests) : query.OrderBy(o => o.NumberOfGuests);
								 break;
							case "NumberOfGuests_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.NumberOfGuests) : query.OrderByDescending(o => o.NumberOfGuests);
                                break;
							case "Revenue":
								query = isOrdered ? ordered.ThenBy(o => o.Revenue) : query.OrderBy(o => o.Revenue);
								 break;
							case "Revenue_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Revenue) : query.OrderByDescending(o => o.Revenue);
                                break;
							case "IsDeleted":
								query = isOrdered ? ordered.ThenBy(o => o.IsDeleted) : query.OrderBy(o => o.IsDeleted);
								 break;
							case "IsDeleted_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsDeleted) : query.OrderByDescending(o => o.IsDeleted);
                                break;
							case "CreatedBy":
								query = isOrdered ? ordered.ThenBy(o => o.CreatedBy) : query.OrderBy(o => o.CreatedBy);
								 break;
							case "CreatedBy_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.CreatedBy) : query.OrderByDescending(o => o.CreatedBy);
                                break;
							case "ModifiedBy":
								query = isOrdered ? ordered.ThenBy(o => o.ModifiedBy) : query.OrderBy(o => o.ModifiedBy);
								 break;
							case "ModifiedBy_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ModifiedBy) : query.OrderByDescending(o => o.ModifiedBy);
                                break;
							case "CreatedDate":
								query = isOrdered ? ordered.ThenBy(o => o.CreatedDate) : query.OrderBy(o => o.CreatedDate);
								 break;
							case "CreatedDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.CreatedDate) : query.OrderByDescending(o => o.CreatedDate);
                                break;
							case "ModifiedDate":
								query = isOrdered ? ordered.ThenBy(o => o.ModifiedDate) : query.OrderBy(o => o.ModifiedDate);
								 break;
							case "ModifiedDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ModifiedDate) : query.OrderByDescending(o => o.ModifiedDate);
                                break;
                            default:
                                if(!isOrdered)
                                    query = query.OrderBy(o => o.Id);
                                break;
                        }
                    }
                    else
                    {
                        query = query.OrderBy(o => o.Id);
                    }
            }
            else
            {
                query = query.OrderBy(o => o.Id);
            }

            return query;
        }

        public static IQueryable<tbl_BI_Operating_MarketResearch> _pagingBuilder(IQueryable<tbl_BI_Operating_MarketResearch> query, Dictionary<string, string> QueryStrings)
        {
            int skip = 0;
            int take = 200;
            if (QueryStrings.Any(d => d.Key == "Skip"))
            {
                int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Skip").Value, out skip);
            }
            if (QueryStrings.Any(d => d.Key == "Take"))
            {
                int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Take").Value, out take);
            }

            query = query.Skip(skip).Take(take);
            return query;
        }

    }

}





