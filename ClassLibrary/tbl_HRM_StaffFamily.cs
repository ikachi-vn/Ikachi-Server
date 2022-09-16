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
    
    
    public partial class tbl_HRM_StaffFamily
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_HRM_StaffFamily()
        {
            this.tbl_HRM_StaffStaffAndFamilyJob = new HashSet<tbl_HRM_StaffStaffAndFamilyJob>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Sort { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public Nullable<int> IDStaff { get; set; }
        public Nullable<int> IDRelative { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string DOB { get; set; }
        public string IdentityCardNumber { get; set; }
        public Nullable<System.DateTime> DateOfIssueID { get; set; }
        public string PlaceOfIssueID { get; set; }
        public Nullable<System.DateTime> DateOfExpiryID { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<System.DateTime> DateOfIssuePassport { get; set; }
        public Nullable<System.DateTime> DateOfExpiryPassport { get; set; }
        public string PlaceOfIssuePassport { get; set; }
        public Nullable<int> IDTypeOfPassport { get; set; }
        public Nullable<int> IDCountryOfIssuePassport { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<bool> IsDependants { get; set; }
        public string HomeAddress { get; set; }
        //List 0:1
        public virtual tbl_HRM_Staff tbl_HRM_Staff { get; set; }
        //List 0:1
        public virtual tbl_LIST_Country tbl_LIST_Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_HRM_StaffStaffAndFamilyJob> tbl_HRM_StaffStaffAndFamilyJob { get; set; }
        //List 0:1
        public virtual tbl_LIST_General tbl_LIST_General { get; set; }
        //List 0:1
        public virtual tbl_LIST_General tbl_LIST_General1 { get; set; }
    }
}


namespace DTOModel
{
	using System;
	public partial class DTO_HRM_StaffFamily
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Remark { get; set; }
		public Nullable<int> Sort { get; set; }
		public bool IsDisabled { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public Nullable<int> IDStaff { get; set; }
		public Nullable<int> IDRelative { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string FullName { get; set; }
		public string ShortName { get; set; }
		public Nullable<bool> Gender { get; set; }
		public string DOB { get; set; }
		public string IdentityCardNumber { get; set; }
		public Nullable<System.DateTime> DateOfIssueID { get; set; }
		public string PlaceOfIssueID { get; set; }
		public Nullable<System.DateTime> DateOfExpiryID { get; set; }
		public string PassportNumber { get; set; }
		public Nullable<System.DateTime> DateOfIssuePassport { get; set; }
		public Nullable<System.DateTime> DateOfExpiryPassport { get; set; }
		public string PlaceOfIssuePassport { get; set; }
		public Nullable<int> IDTypeOfPassport { get; set; }
		public Nullable<int> IDCountryOfIssuePassport { get; set; }
		public Nullable<int> Age { get; set; }
		public Nullable<bool> IsDependants { get; set; }
		public string HomeAddress { get; set; }
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

    public static partial class BS_HRM_StaffFamily 
    {
        public static dynamic getSearch(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch,StaffID, QueryStrings);
        }

		public static IQueryable<DTO_HRM_StaffFamily> get(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
			var query = _queryBuilder(db, IDBranch, StaffID, QueryStrings);
            query = _sortBuilder(query, QueryStrings);
            query = _pagingBuilder(query, QueryStrings);

			return toDTO(query);
        }

        public static List<ItemModel> getValidateData(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch, StaffID, QueryStrings).Select(s => new ItemModel { 
                Id = s.Id,  Code = s.Code,  Name = s.Name, 
            }).ToList();
        }

        public static string export(ExcelPackage package, AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            package.Workbook.Properties.Title = "ART-DMS HRM_StaffFamily";
            package.Workbook.Properties.Author = "hung.vu@codeart.vn";
            package.Workbook.Properties.Application = "ART-DMS";
            package.Workbook.Properties.Company = "A.R.T Distribution";

            ExcelWorkbook workBook = package.Workbook;
            if (workBook != null)
            {
                var ws = workBook.Worksheets.FirstOrDefault();
                var data = _queryBuilder(db, IDBranch, StaffID, QueryStrings).ToList();

                //var HRM_StaffList = BS_HRM_Staff.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var LIST_CountryList = BS_LIST_Country.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var LIST_GeneralList = BS_LIST_General.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var LIST_General1List = BS_LIST_General.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                 

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

		public static DTO_HRM_StaffFamily getAnItem(AppEntities db, int IDBranch, int StaffID, int id)
        {
            var dbResult = db.tbl_HRM_StaffFamily.Find(id);

			return toDTO(dbResult);
			
        }
		
		public static DTO_HRM_StaffFamily getAnItemByCode(AppEntities db, int IDBranch, int StaffID, string code)
        {
            var dbResult = db.tbl_HRM_StaffFamily
			.FirstOrDefault(d => d.IsDeleted == false && d.Code == code );

			return toDTO(dbResult);
			
        }

		public static bool put(AppEntities db, int IDBranch, int StaffID, int Id, dynamic item, string Username, Dictionary<string, string> QueryStrings)
        {
            bool result = false;
            var dbitem = db.tbl_HRM_StaffFamily.Find(Id);
            
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
					errorLog.logMessage("put_HRM_StaffFamily",e);
                    result = false;
                }
            }
            else
                if (QueryStrings.Any(d => d.Key == "ForceCreate"))
                    result =  post(db, IDBranch, StaffID, item, Username) != null;
                
            return result;
        }

        public static void patchDynamicToDB(dynamic item, tbl_HRM_StaffFamily dbitem, string Username)
        {
            Type type = typeof(tbl_HRM_StaffFamily);
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

        public static void patchDTOtoDBValue( DTO_HRM_StaffFamily item, tbl_HRM_StaffFamily dbitem)
        {
            if (item == null){
                dbitem = null;
                return;
            }
            							
			dbitem.Code = item.Code;							
			dbitem.Name = item.Name;							
			dbitem.Remark = item.Remark;							
			dbitem.Sort = item.Sort;							
			dbitem.IsDisabled = item.IsDisabled;							
			dbitem.IsDeleted = item.IsDeleted;							
			dbitem.IDStaff = item.IDStaff;							
			dbitem.IDRelative = item.IDRelative;							
			dbitem.FirstName = item.FirstName;							
			dbitem.LastName = item.LastName;							
			dbitem.MiddleName = item.MiddleName;							
			dbitem.FullName = item.FullName;							
			dbitem.ShortName = item.ShortName;							
			dbitem.Gender = item.Gender;							
			dbitem.DOB = item.DOB;							
			dbitem.IdentityCardNumber = item.IdentityCardNumber;							
			dbitem.DateOfIssueID = item.DateOfIssueID;							
			dbitem.PlaceOfIssueID = item.PlaceOfIssueID;							
			dbitem.DateOfExpiryID = item.DateOfExpiryID;							
			dbitem.PassportNumber = item.PassportNumber;							
			dbitem.DateOfIssuePassport = item.DateOfIssuePassport;							
			dbitem.DateOfExpiryPassport = item.DateOfExpiryPassport;							
			dbitem.PlaceOfIssuePassport = item.PlaceOfIssuePassport;							
			dbitem.IDTypeOfPassport = item.IDTypeOfPassport;							
			dbitem.IDCountryOfIssuePassport = item.IDCountryOfIssuePassport;							
			dbitem.Age = item.Age;							
			dbitem.IsDependants = item.IsDependants;							
			dbitem.HomeAddress = item.HomeAddress;        }

		public static DTO_HRM_StaffFamily post(AppEntities db, int IDBranch, int StaffID, dynamic item, string Username)
        {
            tbl_HRM_StaffFamily dbitem = new tbl_HRM_StaffFamily();
            if (item != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                
				dbitem.CreatedBy = Username;
				dbitem.CreatedDate = DateTime.Now;				
                try
                {
					db.tbl_HRM_StaffFamily.Add(dbitem);
                    db.SaveChanges();
				
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("post_HRM_StaffFamily",e);
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
                Type type = Type.GetType("BaseBusiness.BS_HRM_StaffFamily, ClassLibrary");
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
                        var target = new tbl_HRM_StaffFamily();
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
                    
                    tbl_HRM_StaffFamily dbitem = new tbl_HRM_StaffFamily();
                    if (it.Id == null || string.IsNullOrEmpty(it.Id.Value) || it.Id == "0")
                    {
                        dbitem = new tbl_HRM_StaffFamily();
                        dbitem.CreatedBy = Username;
				        dbitem.CreatedDate = DateTime.Now;
                                            }
                    else
                    {
                        dbitem = db.tbl_HRM_StaffFamily.Find((int)it.Id);
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
                        
                        if (dbitem.Id == 0) db.tbl_HRM_StaffFamily.Add(dbitem);
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
                    errorLog.logMessage("post_HRM_StaffFamily",e);
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

            var dbitems = db.tbl_HRM_StaffFamily.Where(d => IDs.Contains(d.Id));
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
			
                BS_Version.update_Version(db, null, "DTO_HRM_StaffFamily", updateDate, Username);
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("delete_HRM_StaffFamily",e);
                result = false;
            }

            return result;
        }

        public static bool disable(AppEntities db, string ids, bool isDisable, string Username)
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

			
            var dbitems = db.tbl_HRM_StaffFamily.Where(d => IDs.Contains(d.Id));
            var updateDate = DateTime.Now;
            List<int?> IDBranches = new List<int?>();

            foreach (var dbitem in dbitems)
            {
				
				dbitem.ModifiedBy = Username;
				dbitem.ModifiedDate = updateDate;
				
			    dbitem.IsDisabled = isDisable;
                result = true;

                
            }

            try
            {
                db.SaveChanges();
                result = true;
			
                BS_Version.update_Version(db, null, "DTO_HRM_StaffFamily", updateDate, Username);
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("disable_HRM_StaffFamily",e);
                result = false;
            }
            
            return result;
        }

		
		//---//
		public static bool check_Exists(AppEntities db, int id)
		{
             
            return db.tbl_HRM_StaffFamily.Any(e => e.Id == id && e.IsDeleted == false);
            
		}
		
		public static bool check_Exists(AppEntities db, string Code)
		{
             
            return db.tbl_HRM_StaffFamily.Any(e => e.Code == Code && e.IsDeleted == false);
            
		}
		
		//---//
		public static IQueryable<DTO_HRM_StaffFamily> toDTO(IQueryable<tbl_HRM_StaffFamily> query)
        {
			return query
			.Select(s => new DTO_HRM_StaffFamily(){							
				Id = s.Id,							
				Code = s.Code,							
				Name = s.Name,							
				Remark = s.Remark,							
				Sort = s.Sort,							
				IsDisabled = s.IsDisabled,							
				IsDeleted = s.IsDeleted,							
				CreatedBy = s.CreatedBy,							
				ModifiedBy = s.ModifiedBy,							
				CreatedDate = s.CreatedDate,							
				ModifiedDate = s.ModifiedDate,							
				IDStaff = s.IDStaff,							
				IDRelative = s.IDRelative,							
				FirstName = s.FirstName,							
				LastName = s.LastName,							
				MiddleName = s.MiddleName,							
				FullName = s.FullName,							
				ShortName = s.ShortName,							
				Gender = s.Gender,							
				DOB = s.DOB,							
				IdentityCardNumber = s.IdentityCardNumber,							
				DateOfIssueID = s.DateOfIssueID,							
				PlaceOfIssueID = s.PlaceOfIssueID,							
				DateOfExpiryID = s.DateOfExpiryID,							
				PassportNumber = s.PassportNumber,							
				DateOfIssuePassport = s.DateOfIssuePassport,							
				DateOfExpiryPassport = s.DateOfExpiryPassport,							
				PlaceOfIssuePassport = s.PlaceOfIssuePassport,							
				IDTypeOfPassport = s.IDTypeOfPassport,							
				IDCountryOfIssuePassport = s.IDCountryOfIssuePassport,							
				Age = s.Age,							
				IsDependants = s.IsDependants,							
				HomeAddress = s.HomeAddress,					
			});//.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                              
        }

		public static DTO_HRM_StaffFamily toDTO(tbl_HRM_StaffFamily dbResult)
        {
			if (dbResult != null)
			{
				return new DTO_HRM_StaffFamily()
				{							
					Id = dbResult.Id,							
					Code = dbResult.Code,							
					Name = dbResult.Name,							
					Remark = dbResult.Remark,							
					Sort = dbResult.Sort,							
					IsDisabled = dbResult.IsDisabled,							
					IsDeleted = dbResult.IsDeleted,							
					CreatedBy = dbResult.CreatedBy,							
					ModifiedBy = dbResult.ModifiedBy,							
					CreatedDate = dbResult.CreatedDate,							
					ModifiedDate = dbResult.ModifiedDate,							
					IDStaff = dbResult.IDStaff,							
					IDRelative = dbResult.IDRelative,							
					FirstName = dbResult.FirstName,							
					LastName = dbResult.LastName,							
					MiddleName = dbResult.MiddleName,							
					FullName = dbResult.FullName,							
					ShortName = dbResult.ShortName,							
					Gender = dbResult.Gender,							
					DOB = dbResult.DOB,							
					IdentityCardNumber = dbResult.IdentityCardNumber,							
					DateOfIssueID = dbResult.DateOfIssueID,							
					PlaceOfIssueID = dbResult.PlaceOfIssueID,							
					DateOfExpiryID = dbResult.DateOfExpiryID,							
					PassportNumber = dbResult.PassportNumber,							
					DateOfIssuePassport = dbResult.DateOfIssuePassport,							
					DateOfExpiryPassport = dbResult.DateOfExpiryPassport,							
					PlaceOfIssuePassport = dbResult.PlaceOfIssuePassport,							
					IDTypeOfPassport = dbResult.IDTypeOfPassport,							
					IDCountryOfIssuePassport = dbResult.IDCountryOfIssuePassport,							
					Age = dbResult.Age,							
					IsDependants = dbResult.IsDependants,							
					HomeAddress = dbResult.HomeAddress,
				};
			}
			else
				return null; 
        }

		public static IQueryable<tbl_HRM_StaffFamily> _queryBuilder(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            
			IQueryable<tbl_HRM_StaffFamily> query = db.tbl_HRM_StaffFamily.AsNoTracking();//.Where(d => d.IsDeleted == false );
			

			//Query keyword
			if (QueryStrings.Any(d => d.Key == "Keyword") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value;
                query = query.Where(d=>d.Name.Contains(keyword) || d.Code.Contains(keyword));
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

			//Query Code (string)
			if (QueryStrings.Any(d => d.Key == "Code_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Code_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Code_eq").Value;
                query = query.Where(d=>d.Code == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "Code") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Code").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Code").Value;
                query = query.Where(d=>d.Code.Contains(keyword));
            }
            

			//Query Name (string)
			if (QueryStrings.Any(d => d.Key == "Name_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Name_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Name_eq").Value;
                query = query.Where(d=>d.Name == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "Name") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Name").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Name").Value;
                query = query.Where(d=>d.Name.Contains(keyword));
            }
            

			//Query Remark (string)
			if (QueryStrings.Any(d => d.Key == "Remark_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Remark_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Remark_eq").Value;
                query = query.Where(d=>d.Remark == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "Remark") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Remark").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Remark").Value;
                query = query.Where(d=>d.Remark.Contains(keyword));
            }
            

			//Query Sort (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "SortFrom") && QueryStrings.Any(d => d.Key == "SortTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "SortFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "SortTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Sort && d.Sort <= toVal);

			//Query IsDisabled (bool)
			if (QueryStrings.Any(d => d.Key == "IsDisabled"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "IsDisabled").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.IsDisabled);
            }
            else
                query = query.Where(d => false == d.IsDisabled);

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


			//Query IDStaff (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDStaff"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDStaff").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDStaff));
////                    query = query.Where(d => IDs.Contains(d.IDStaff));
//                    
            }

			//Query IDRelative (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDRelative"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDRelative").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDRelative));
////                    query = query.Where(d => IDs.Contains(d.IDRelative));
//                    
            }

			//Query FirstName (string)
			if (QueryStrings.Any(d => d.Key == "FirstName_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "FirstName_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "FirstName_eq").Value;
                query = query.Where(d=>d.FirstName == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "FirstName") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "FirstName").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "FirstName").Value;
                query = query.Where(d=>d.FirstName.Contains(keyword));
            }
            

			//Query LastName (string)
			if (QueryStrings.Any(d => d.Key == "LastName_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "LastName_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "LastName_eq").Value;
                query = query.Where(d=>d.LastName == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "LastName") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "LastName").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "LastName").Value;
                query = query.Where(d=>d.LastName.Contains(keyword));
            }
            

			//Query MiddleName (string)
			if (QueryStrings.Any(d => d.Key == "MiddleName_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "MiddleName_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "MiddleName_eq").Value;
                query = query.Where(d=>d.MiddleName == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "MiddleName") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "MiddleName").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "MiddleName").Value;
                query = query.Where(d=>d.MiddleName.Contains(keyword));
            }
            

			//Query FullName (string)
			if (QueryStrings.Any(d => d.Key == "FullName_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "FullName_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "FullName_eq").Value;
                query = query.Where(d=>d.FullName == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "FullName") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "FullName").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "FullName").Value;
                query = query.Where(d=>d.FullName.Contains(keyword));
            }
            

			//Query ShortName (string)
			if (QueryStrings.Any(d => d.Key == "ShortName_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ShortName_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ShortName_eq").Value;
                query = query.Where(d=>d.ShortName == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "ShortName") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ShortName").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ShortName").Value;
                query = query.Where(d=>d.ShortName.Contains(keyword));
            }
            

			//Query Gender (Nullable<bool>)

			//Query DOB (string)
			if (QueryStrings.Any(d => d.Key == "DOB_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "DOB_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "DOB_eq").Value;
                query = query.Where(d=>d.DOB == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "DOB") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "DOB").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "DOB").Value;
                query = query.Where(d=>d.DOB.Contains(keyword));
            }
            

			//Query IdentityCardNumber (string)
			if (QueryStrings.Any(d => d.Key == "IdentityCardNumber_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "IdentityCardNumber_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "IdentityCardNumber_eq").Value;
                query = query.Where(d=>d.IdentityCardNumber == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "IdentityCardNumber") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "IdentityCardNumber").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "IdentityCardNumber").Value;
                query = query.Where(d=>d.IdentityCardNumber.Contains(keyword));
            }
            

			//Query DateOfIssueID (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "DateOfIssueIDFrom") && QueryStrings.Any(d => d.Key == "DateOfIssueIDTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssueIDFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssueIDTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.DateOfIssueID && d.DateOfIssueID <= toDate);

            if (QueryStrings.Any(d => d.Key == "DateOfIssueID"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssueID").Value, out DateTime val))
                    query = query.Where(d => d.DateOfIssueID != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.DateOfIssueID));


			//Query PlaceOfIssueID (string)
			if (QueryStrings.Any(d => d.Key == "PlaceOfIssueID_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssueID_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssueID_eq").Value;
                query = query.Where(d=>d.PlaceOfIssueID == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PlaceOfIssueID") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssueID").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssueID").Value;
                query = query.Where(d=>d.PlaceOfIssueID.Contains(keyword));
            }
            

			//Query DateOfExpiryID (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "DateOfExpiryIDFrom") && QueryStrings.Any(d => d.Key == "DateOfExpiryIDTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryIDFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryIDTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.DateOfExpiryID && d.DateOfExpiryID <= toDate);

            if (QueryStrings.Any(d => d.Key == "DateOfExpiryID"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryID").Value, out DateTime val))
                    query = query.Where(d => d.DateOfExpiryID != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.DateOfExpiryID));


			//Query PassportNumber (string)
			if (QueryStrings.Any(d => d.Key == "PassportNumber_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PassportNumber_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PassportNumber_eq").Value;
                query = query.Where(d=>d.PassportNumber == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PassportNumber") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PassportNumber").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PassportNumber").Value;
                query = query.Where(d=>d.PassportNumber.Contains(keyword));
            }
            

			//Query DateOfIssuePassport (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "DateOfIssuePassportFrom") && QueryStrings.Any(d => d.Key == "DateOfIssuePassportTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssuePassportFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssuePassportTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.DateOfIssuePassport && d.DateOfIssuePassport <= toDate);

            if (QueryStrings.Any(d => d.Key == "DateOfIssuePassport"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfIssuePassport").Value, out DateTime val))
                    query = query.Where(d => d.DateOfIssuePassport != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.DateOfIssuePassport));


			//Query DateOfExpiryPassport (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "DateOfExpiryPassportFrom") && QueryStrings.Any(d => d.Key == "DateOfExpiryPassportTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryPassportFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryPassportTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.DateOfExpiryPassport && d.DateOfExpiryPassport <= toDate);

            if (QueryStrings.Any(d => d.Key == "DateOfExpiryPassport"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "DateOfExpiryPassport").Value, out DateTime val))
                    query = query.Where(d => d.DateOfExpiryPassport != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.DateOfExpiryPassport));


			//Query PlaceOfIssuePassport (string)
			if (QueryStrings.Any(d => d.Key == "PlaceOfIssuePassport_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssuePassport_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssuePassport_eq").Value;
                query = query.Where(d=>d.PlaceOfIssuePassport == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PlaceOfIssuePassport") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssuePassport").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PlaceOfIssuePassport").Value;
                query = query.Where(d=>d.PlaceOfIssuePassport.Contains(keyword));
            }
            

			//Query IDTypeOfPassport (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDTypeOfPassport"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDTypeOfPassport").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDTypeOfPassport));
////                    query = query.Where(d => IDs.Contains(d.IDTypeOfPassport));
//                    
            }

			//Query IDCountryOfIssuePassport (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDCountryOfIssuePassport"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDCountryOfIssuePassport").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDCountryOfIssuePassport));
////                    query = query.Where(d => IDs.Contains(d.IDCountryOfIssuePassport));
//                    
            }

			//Query Age (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "AgeFrom") && QueryStrings.Any(d => d.Key == "AgeTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "AgeFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "AgeTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Age && d.Age <= toVal);

			//Query IsDependants (Nullable<bool>)

			//Query HomeAddress (string)
			if (QueryStrings.Any(d => d.Key == "HomeAddress_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "HomeAddress_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "HomeAddress_eq").Value;
                query = query.Where(d=>d.HomeAddress == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "HomeAddress") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "HomeAddress").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "HomeAddress").Value;
                query = query.Where(d=>d.HomeAddress.Contains(keyword));
            }
            
            return query;
        }
		
        public static IQueryable<tbl_HRM_StaffFamily> _sortBuilder(IQueryable<tbl_HRM_StaffFamily> query, Dictionary<string, string> QueryStrings)
        {
            if (QueryStrings.Any(d => d.Key == "SortBy"))
            {
                var sorts = QueryStrings.FirstOrDefault(d => d.Key == "SortBy").Value.Replace("[", "").Replace("]", "").Split(',');
                
                foreach (var item in sorts)
                    if (!string.IsNullOrEmpty(item))
                    {
                        var ordered = query as IOrderedQueryable<tbl_HRM_StaffFamily>;
                        bool isOrdered = ordered.ToString().IndexOf("ORDER BY") !=-1;

                        switch (item)
                        {
							case "Id":
								query = isOrdered ? ordered.ThenBy(o => o.Id) : query.OrderBy(o => o.Id);
								 break;
							case "Id_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Id) : query.OrderByDescending(o => o.Id);
                                break;
							case "Code":
								query = isOrdered ? ordered.ThenBy(o => o.Code) : query.OrderBy(o => o.Code);
								 break;
							case "Code_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Code) : query.OrderByDescending(o => o.Code);
                                break;
							case "Name":
								query = isOrdered ? ordered.ThenBy(o => o.Name) : query.OrderBy(o => o.Name);
								 break;
							case "Name_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Name) : query.OrderByDescending(o => o.Name);
                                break;
							case "Remark":
								query = isOrdered ? ordered.ThenBy(o => o.Remark) : query.OrderBy(o => o.Remark);
								 break;
							case "Remark_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Remark) : query.OrderByDescending(o => o.Remark);
                                break;
							case "Sort":
								query = isOrdered ? ordered.ThenBy(o => o.Sort) : query.OrderBy(o => o.Sort);
								 break;
							case "Sort_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Sort) : query.OrderByDescending(o => o.Sort);
                                break;
							case "IsDisabled":
								query = isOrdered ? ordered.ThenBy(o => o.IsDisabled) : query.OrderBy(o => o.IsDisabled);
								 break;
							case "IsDisabled_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsDisabled) : query.OrderByDescending(o => o.IsDisabled);
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
							case "IDStaff":
								query = isOrdered ? ordered.ThenBy(o => o.IDStaff) : query.OrderBy(o => o.IDStaff);
								 break;
							case "IDStaff_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDStaff) : query.OrderByDescending(o => o.IDStaff);
                                break;
							case "IDRelative":
								query = isOrdered ? ordered.ThenBy(o => o.IDRelative) : query.OrderBy(o => o.IDRelative);
								 break;
							case "IDRelative_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDRelative) : query.OrderByDescending(o => o.IDRelative);
                                break;
							case "FirstName":
								query = isOrdered ? ordered.ThenBy(o => o.FirstName) : query.OrderBy(o => o.FirstName);
								 break;
							case "FirstName_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.FirstName) : query.OrderByDescending(o => o.FirstName);
                                break;
							case "LastName":
								query = isOrdered ? ordered.ThenBy(o => o.LastName) : query.OrderBy(o => o.LastName);
								 break;
							case "LastName_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.LastName) : query.OrderByDescending(o => o.LastName);
                                break;
							case "MiddleName":
								query = isOrdered ? ordered.ThenBy(o => o.MiddleName) : query.OrderBy(o => o.MiddleName);
								 break;
							case "MiddleName_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.MiddleName) : query.OrderByDescending(o => o.MiddleName);
                                break;
							case "FullName":
								query = isOrdered ? ordered.ThenBy(o => o.FullName) : query.OrderBy(o => o.FullName);
								 break;
							case "FullName_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.FullName) : query.OrderByDescending(o => o.FullName);
                                break;
							case "ShortName":
								query = isOrdered ? ordered.ThenBy(o => o.ShortName) : query.OrderBy(o => o.ShortName);
								 break;
							case "ShortName_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ShortName) : query.OrderByDescending(o => o.ShortName);
                                break;
							case "Gender":
								query = isOrdered ? ordered.ThenBy(o => o.Gender) : query.OrderBy(o => o.Gender);
								 break;
							case "Gender_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Gender) : query.OrderByDescending(o => o.Gender);
                                break;
							case "DOB":
								query = isOrdered ? ordered.ThenBy(o => o.DOB) : query.OrderBy(o => o.DOB);
								 break;
							case "DOB_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.DOB) : query.OrderByDescending(o => o.DOB);
                                break;
							case "IdentityCardNumber":
								query = isOrdered ? ordered.ThenBy(o => o.IdentityCardNumber) : query.OrderBy(o => o.IdentityCardNumber);
								 break;
							case "IdentityCardNumber_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IdentityCardNumber) : query.OrderByDescending(o => o.IdentityCardNumber);
                                break;
							case "DateOfIssueID":
								query = isOrdered ? ordered.ThenBy(o => o.DateOfIssueID) : query.OrderBy(o => o.DateOfIssueID);
								 break;
							case "DateOfIssueID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.DateOfIssueID) : query.OrderByDescending(o => o.DateOfIssueID);
                                break;
							case "PlaceOfIssueID":
								query = isOrdered ? ordered.ThenBy(o => o.PlaceOfIssueID) : query.OrderBy(o => o.PlaceOfIssueID);
								 break;
							case "PlaceOfIssueID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PlaceOfIssueID) : query.OrderByDescending(o => o.PlaceOfIssueID);
                                break;
							case "DateOfExpiryID":
								query = isOrdered ? ordered.ThenBy(o => o.DateOfExpiryID) : query.OrderBy(o => o.DateOfExpiryID);
								 break;
							case "DateOfExpiryID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.DateOfExpiryID) : query.OrderByDescending(o => o.DateOfExpiryID);
                                break;
							case "PassportNumber":
								query = isOrdered ? ordered.ThenBy(o => o.PassportNumber) : query.OrderBy(o => o.PassportNumber);
								 break;
							case "PassportNumber_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PassportNumber) : query.OrderByDescending(o => o.PassportNumber);
                                break;
							case "DateOfIssuePassport":
								query = isOrdered ? ordered.ThenBy(o => o.DateOfIssuePassport) : query.OrderBy(o => o.DateOfIssuePassport);
								 break;
							case "DateOfIssuePassport_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.DateOfIssuePassport) : query.OrderByDescending(o => o.DateOfIssuePassport);
                                break;
							case "DateOfExpiryPassport":
								query = isOrdered ? ordered.ThenBy(o => o.DateOfExpiryPassport) : query.OrderBy(o => o.DateOfExpiryPassport);
								 break;
							case "DateOfExpiryPassport_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.DateOfExpiryPassport) : query.OrderByDescending(o => o.DateOfExpiryPassport);
                                break;
							case "PlaceOfIssuePassport":
								query = isOrdered ? ordered.ThenBy(o => o.PlaceOfIssuePassport) : query.OrderBy(o => o.PlaceOfIssuePassport);
								 break;
							case "PlaceOfIssuePassport_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PlaceOfIssuePassport) : query.OrderByDescending(o => o.PlaceOfIssuePassport);
                                break;
							case "IDTypeOfPassport":
								query = isOrdered ? ordered.ThenBy(o => o.IDTypeOfPassport) : query.OrderBy(o => o.IDTypeOfPassport);
								 break;
							case "IDTypeOfPassport_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDTypeOfPassport) : query.OrderByDescending(o => o.IDTypeOfPassport);
                                break;
							case "IDCountryOfIssuePassport":
								query = isOrdered ? ordered.ThenBy(o => o.IDCountryOfIssuePassport) : query.OrderBy(o => o.IDCountryOfIssuePassport);
								 break;
							case "IDCountryOfIssuePassport_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDCountryOfIssuePassport) : query.OrderByDescending(o => o.IDCountryOfIssuePassport);
                                break;
							case "Age":
								query = isOrdered ? ordered.ThenBy(o => o.Age) : query.OrderBy(o => o.Age);
								 break;
							case "Age_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Age) : query.OrderByDescending(o => o.Age);
                                break;
							case "IsDependants":
								query = isOrdered ? ordered.ThenBy(o => o.IsDependants) : query.OrderBy(o => o.IsDependants);
								 break;
							case "IsDependants_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsDependants) : query.OrderByDescending(o => o.IsDependants);
                                break;
							case "HomeAddress":
								query = isOrdered ? ordered.ThenBy(o => o.HomeAddress) : query.OrderBy(o => o.HomeAddress);
								 break;
							case "HomeAddress_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.HomeAddress) : query.OrderByDescending(o => o.HomeAddress);
                                break;
                            default:
                                if(!isOrdered)
                                    query = query.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                                break;
                        }
                    }
                    else
                    {
                        query = query.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                    }
            }
            else
            {
                query = query.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
            }

            return query;
        }

        public static IQueryable<tbl_HRM_StaffFamily> _pagingBuilder(IQueryable<tbl_HRM_StaffFamily> query, Dictionary<string, string> QueryStrings)
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






