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
    
    
    public partial class tbl_WMS_PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_WMS_PriceList()
        {
            this.tbl_BANK_PaymentTerm = new HashSet<tbl_BANK_PaymentTerm>();
            this.tbl_CRM_BusinessPartnerGroup = new HashSet<tbl_CRM_BusinessPartnerGroup>();
            this.tbl_CRM_Contact = new HashSet<tbl_CRM_Contact>();
            this.tbl_CRM_Contact1 = new HashSet<tbl_CRM_Contact>();
            this.tbl_WMS_PriceListDetail = new HashSet<tbl_WMS_PriceListDetail>();
            this.tbl_WMS_PriceListVersion = new HashSet<tbl_WMS_PriceListVersion>();
        }
    
        public Nullable<int> IDBranch { get; set; }
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
        public Nullable<int> IDBasePriceList { get; set; }
        public Nullable<decimal> Factor { get; set; }
        public Nullable<int> RoundingMethod { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public string PrimaryDefaultCurrency { get; set; }
        public string PrimaryDefaultCurrency1 { get; set; }
        public string PrimaryDefaultCurrency2 { get; set; }
        public bool IsPriceListForVendor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_BANK_PaymentTerm> tbl_BANK_PaymentTerm { get; set; }
        //List 0:1
        public virtual tbl_BRA_Branch tbl_BRA_Branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_BusinessPartnerGroup> tbl_CRM_BusinessPartnerGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_Contact> tbl_CRM_Contact { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_Contact> tbl_CRM_Contact1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_WMS_PriceListDetail> tbl_WMS_PriceListDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_WMS_PriceListVersion> tbl_WMS_PriceListVersion { get; set; }
    }
}


namespace DTOModel
{
	using System;
	public partial class DTO_WMS_PriceList
	{
		public Nullable<int> IDBranch { get; set; }
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
		public Nullable<int> IDBasePriceList { get; set; }
		public Nullable<decimal> Factor { get; set; }
		public Nullable<int> RoundingMethod { get; set; }
		public Nullable<System.DateTime> ValidFrom { get; set; }
		public Nullable<System.DateTime> ValidTo { get; set; }
		public string PrimaryDefaultCurrency { get; set; }
		public string PrimaryDefaultCurrency1 { get; set; }
		public string PrimaryDefaultCurrency2 { get; set; }
		public bool IsPriceListForVendor { get; set; }
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

    public static partial class BS_WMS_PriceList 
    {
        public static dynamic getSearch(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch,StaffID, QueryStrings);
        }

		public static IQueryable<DTO_WMS_PriceList> get(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
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
            package.Workbook.Properties.Title = "ART-DMS WMS_PriceList";
            package.Workbook.Properties.Author = "hung.vu@codeart.vn";
            package.Workbook.Properties.Application = "ART-DMS";
            package.Workbook.Properties.Company = "A.R.T Distribution";

            ExcelWorkbook workBook = package.Workbook;
            if (workBook != null)
            {
                var ws = workBook.Worksheets.FirstOrDefault();
                var data = _queryBuilder(db, IDBranch, StaffID, QueryStrings).ToList();

                //var BRA_BranchList = BS_BRA_Branch.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                 

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

		public static DTO_WMS_PriceList getAnItem(AppEntities db, int IDBranch, int StaffID, int id)
        {
            var dbResult = db.tbl_WMS_PriceList.Find(id);

			//if (dbResult == null || (IDBranch != 0 && dbResult.IDBranch != IDBranch))
			//	return null; 
			//else 
				return toDTO(dbResult);
			
        }
		
		public static DTO_WMS_PriceList getAnItemByCode(AppEntities db, int IDBranch, int StaffID, string code)
        {
            var dbResult = db.tbl_WMS_PriceList
			.FirstOrDefault(d => d.IsDeleted == false && d.Code == code); //.FirstOrDefault(d => d.IsDeleted == false && (d.IDBranch == IDBranch || IDBranch == 0) && d.Code == code);

			
            return toDTO(dbResult);
            //if (dbResult == null || dbResult.IDBranch != IDBranch)
			//	return null; 
			//else 
			//	return toDTO(dbResult);

			
        }

		public static bool put(AppEntities db, int IDBranch, int StaffID, int Id, dynamic item, string Username, Dictionary<string, string> QueryStrings)
        {
            bool result = false;
            var dbitem = db.tbl_WMS_PriceList.Find(Id);
            
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
					errorLog.logMessage("put_WMS_PriceList",e);
                    result = false;
                }
            }
            else
                if (QueryStrings.Any(d => d.Key == "ForceCreate"))
                    result =  post(db, IDBranch, StaffID, item, Username) != null;
                
            return result;
        }

        public static void patchDynamicToDB(dynamic item, tbl_WMS_PriceList dbitem, string Username)
        {
            Type type = typeof(tbl_WMS_PriceList);
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

        public static void patchDTOtoDBValue( DTO_WMS_PriceList item, tbl_WMS_PriceList dbitem)
        {
            if (item == null){
                dbitem = null;
                return;
            }
            							
			dbitem.IDBranch = item.IDBranch;							
			dbitem.Code = item.Code;							
			dbitem.Name = item.Name;							
			dbitem.Remark = item.Remark;							
			dbitem.Sort = item.Sort;							
			dbitem.IsDisabled = item.IsDisabled;							
			dbitem.IsDeleted = item.IsDeleted;							
			dbitem.IDBasePriceList = item.IDBasePriceList;							
			dbitem.Factor = item.Factor;							
			dbitem.RoundingMethod = item.RoundingMethod;							
			dbitem.ValidFrom = item.ValidFrom;							
			dbitem.ValidTo = item.ValidTo;							
			dbitem.PrimaryDefaultCurrency = item.PrimaryDefaultCurrency;							
			dbitem.PrimaryDefaultCurrency1 = item.PrimaryDefaultCurrency1;							
			dbitem.PrimaryDefaultCurrency2 = item.PrimaryDefaultCurrency2;							
			dbitem.IsPriceListForVendor = item.IsPriceListForVendor;        }

		public static DTO_WMS_PriceList post(AppEntities db, int IDBranch, int StaffID, dynamic item, string Username)
        {
            tbl_WMS_PriceList dbitem = new tbl_WMS_PriceList();
            if (item != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                
				dbitem.CreatedBy = Username;
				dbitem.CreatedDate = DateTime.Now;				
                try
                {
					db.tbl_WMS_PriceList.Add(dbitem);
                    db.SaveChanges();
				
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("post_WMS_PriceList",e);
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
                Type type = Type.GetType("BaseBusiness.BS_WMS_PriceList, ClassLibrary");
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
                        var target = new tbl_WMS_PriceList();
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
                    
                    tbl_WMS_PriceList dbitem = new tbl_WMS_PriceList();
                    if (it.Id == null || string.IsNullOrEmpty(it.Id.Value) || it.Id == "0")
                    {
                        dbitem = new tbl_WMS_PriceList();
                        dbitem.CreatedBy = Username;
				        dbitem.CreatedDate = DateTime.Now;
                                            }
                    else
                    {
                        dbitem = db.tbl_WMS_PriceList.Find((int)it.Id);
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
                        
                        if (dbitem.Id == 0) db.tbl_WMS_PriceList.Add(dbitem);
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
                    errorLog.logMessage("post_WMS_PriceList",e);
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

            var dbitems = db.tbl_WMS_PriceList.Where(d => IDs.Contains(d.Id));
            var updateDate = DateTime.Now;
            List<int?> IDBranches = new List<int?>();

            foreach (var dbitem in dbitems)
            {
							
				dbitem.ModifiedBy = Username;
				dbitem.ModifiedDate = updateDate;
				dbitem.IsDeleted = true;
							            
                if (!IDBranches.Any(d=>d==dbitem.IDBranch))
                {
                    IDBranches.Add(dbitem.IDBranch);
                }
			
                
            }

            try
            {
                db.SaveChanges();
                result = true;
			
                foreach (var IDBranch in IDBranches)
                {
                    BS_Version.update_Version(db, IDBranch, "DTO_WMS_PriceList", updateDate, Username);
                }
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("delete_WMS_PriceList",e);
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

			
            var dbitems = db.tbl_WMS_PriceList.Where(d => IDs.Contains(d.Id));
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
			
                foreach (var IDBranch in IDBranches)
                {
                    BS_Version.update_Version(db, IDBranch, "DTO_WMS_PriceList", updateDate, Username);
                }
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("disable_WMS_PriceList",e);
                result = false;
            }
            
            return result;
        }

		
		//---//
		public static bool check_Exists(AppEntities db, int id)
		{
             
            return db.tbl_WMS_PriceList.Any(e => e.Id == id && e.IsDeleted == false);
            
		}
		
		public static bool check_Exists(AppEntities db, string Code)
		{
             
            return db.tbl_WMS_PriceList.Any(e => e.Code == Code && e.IsDeleted == false);
            
		}
		
		//---//
		public static IQueryable<DTO_WMS_PriceList> toDTO(IQueryable<tbl_WMS_PriceList> query)
        {
			return query
			.Select(s => new DTO_WMS_PriceList(){							
				IDBranch = s.IDBranch,							
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
				IDBasePriceList = s.IDBasePriceList,							
				Factor = s.Factor,							
				RoundingMethod = s.RoundingMethod,							
				ValidFrom = s.ValidFrom,							
				ValidTo = s.ValidTo,							
				PrimaryDefaultCurrency = s.PrimaryDefaultCurrency,							
				PrimaryDefaultCurrency1 = s.PrimaryDefaultCurrency1,							
				PrimaryDefaultCurrency2 = s.PrimaryDefaultCurrency2,							
				IsPriceListForVendor = s.IsPriceListForVendor,					
			});//.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                              
        }

		public static DTO_WMS_PriceList toDTO(tbl_WMS_PriceList dbResult)
        {
			if (dbResult != null)
			{
				return new DTO_WMS_PriceList()
				{							
					IDBranch = dbResult.IDBranch,							
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
					IDBasePriceList = dbResult.IDBasePriceList,							
					Factor = dbResult.Factor,							
					RoundingMethod = dbResult.RoundingMethod,							
					ValidFrom = dbResult.ValidFrom,							
					ValidTo = dbResult.ValidTo,							
					PrimaryDefaultCurrency = dbResult.PrimaryDefaultCurrency,							
					PrimaryDefaultCurrency1 = dbResult.PrimaryDefaultCurrency1,							
					PrimaryDefaultCurrency2 = dbResult.PrimaryDefaultCurrency2,							
					IsPriceListForVendor = dbResult.IsPriceListForVendor,
				};
			}
			else
				return null; 
        }

		public static IQueryable<tbl_WMS_PriceList> _queryBuilder(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            
			IQueryable<tbl_WMS_PriceList> query = db.tbl_WMS_PriceList.AsNoTracking();//.Where(d => d.IsDeleted == false);
			

			//Query keyword
			if (QueryStrings.Any(d => d.Key == "Keyword") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value;
                query = query.Where(d=>d.Name.Contains(keyword) || d.Code.Contains(keyword));
            }



			//Query IDBranch (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDBranch"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDBranch").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDBranch));
////                    query = query.Where(d => d.IDBranch == null || IDs.Contains(d.IDBranch));
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


			//Query IDBasePriceList (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDBasePriceList"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDBasePriceList").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDBasePriceList));
////                    query = query.Where(d => IDs.Contains(d.IDBasePriceList));
//                    
            }

			//Query Factor (Nullable<decimal>)
			if (QueryStrings.Any(d => d.Key == "FactorFrom") && QueryStrings.Any(d => d.Key == "FactorTo"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "FactorFrom").Value, out decimal fromVal) && decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "FactorTo").Value, out decimal toVal))
                    query = query.Where(d => fromVal <= d.Factor && d.Factor <= toVal);

			//Query RoundingMethod (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "RoundingMethodFrom") && QueryStrings.Any(d => d.Key == "RoundingMethodTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "RoundingMethodFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "RoundingMethodTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.RoundingMethod && d.RoundingMethod <= toVal);

			//Query ValidFrom (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "ValidFromFrom") && QueryStrings.Any(d => d.Key == "ValidFromTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidFromFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidFromTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.ValidFrom && d.ValidFrom <= toDate);

            if (QueryStrings.Any(d => d.Key == "ValidFrom"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidFrom").Value, out DateTime val))
                    query = query.Where(d => d.ValidFrom != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.ValidFrom));


			//Query ValidTo (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "ValidToFrom") && QueryStrings.Any(d => d.Key == "ValidToTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidToFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidToTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.ValidTo && d.ValidTo <= toDate);

            if (QueryStrings.Any(d => d.Key == "ValidTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ValidTo").Value, out DateTime val))
                    query = query.Where(d => d.ValidTo != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.ValidTo));


			//Query PrimaryDefaultCurrency (string)
			if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency_eq").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency.Contains(keyword));
            }
            

			//Query PrimaryDefaultCurrency1 (string)
			if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency1_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency1_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency1_eq").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency1 == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency1") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency1").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency1").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency1.Contains(keyword));
            }
            

			//Query PrimaryDefaultCurrency2 (string)
			if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency2_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency2_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency2_eq").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency2 == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "PrimaryDefaultCurrency2") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency2").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "PrimaryDefaultCurrency2").Value;
                query = query.Where(d=>d.PrimaryDefaultCurrency2.Contains(keyword));
            }
            

			//Query IsPriceListForVendor (bool)
			if (QueryStrings.Any(d => d.Key == "IsPriceListForVendor"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "IsPriceListForVendor").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.IsPriceListForVendor);
            }
            return query;
        }
		
        public static IQueryable<tbl_WMS_PriceList> _sortBuilder(IQueryable<tbl_WMS_PriceList> query, Dictionary<string, string> QueryStrings)
        {
            if (QueryStrings.Any(d => d.Key == "SortBy"))
            {
                var sorts = QueryStrings.FirstOrDefault(d => d.Key == "SortBy").Value.Replace("[", "").Replace("]", "").Split(',');
                
                foreach (var item in sorts)
                    if (!string.IsNullOrEmpty(item))
                    {
                        var ordered = query as IOrderedQueryable<tbl_WMS_PriceList>;
                        bool isOrdered = ordered.ToString().IndexOf("ORDER BY") !=-1;

                        switch (item)
                        {
							case "IDBranch":
								query = isOrdered ? ordered.ThenBy(o => o.IDBranch) : query.OrderBy(o => o.IDBranch);
								 break;
							case "IDBranch_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDBranch) : query.OrderByDescending(o => o.IDBranch);
                                break;
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
							case "IDBasePriceList":
								query = isOrdered ? ordered.ThenBy(o => o.IDBasePriceList) : query.OrderBy(o => o.IDBasePriceList);
								 break;
							case "IDBasePriceList_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDBasePriceList) : query.OrderByDescending(o => o.IDBasePriceList);
                                break;
							case "Factor":
								query = isOrdered ? ordered.ThenBy(o => o.Factor) : query.OrderBy(o => o.Factor);
								 break;
							case "Factor_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Factor) : query.OrderByDescending(o => o.Factor);
                                break;
							case "RoundingMethod":
								query = isOrdered ? ordered.ThenBy(o => o.RoundingMethod) : query.OrderBy(o => o.RoundingMethod);
								 break;
							case "RoundingMethod_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RoundingMethod) : query.OrderByDescending(o => o.RoundingMethod);
                                break;
							case "ValidFrom":
								query = isOrdered ? ordered.ThenBy(o => o.ValidFrom) : query.OrderBy(o => o.ValidFrom);
								 break;
							case "ValidFrom_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ValidFrom) : query.OrderByDescending(o => o.ValidFrom);
                                break;
							case "ValidTo":
								query = isOrdered ? ordered.ThenBy(o => o.ValidTo) : query.OrderBy(o => o.ValidTo);
								 break;
							case "ValidTo_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ValidTo) : query.OrderByDescending(o => o.ValidTo);
                                break;
							case "PrimaryDefaultCurrency":
								query = isOrdered ? ordered.ThenBy(o => o.PrimaryDefaultCurrency) : query.OrderBy(o => o.PrimaryDefaultCurrency);
								 break;
							case "PrimaryDefaultCurrency_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PrimaryDefaultCurrency) : query.OrderByDescending(o => o.PrimaryDefaultCurrency);
                                break;
							case "PrimaryDefaultCurrency1":
								query = isOrdered ? ordered.ThenBy(o => o.PrimaryDefaultCurrency1) : query.OrderBy(o => o.PrimaryDefaultCurrency1);
								 break;
							case "PrimaryDefaultCurrency1_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PrimaryDefaultCurrency1) : query.OrderByDescending(o => o.PrimaryDefaultCurrency1);
                                break;
							case "PrimaryDefaultCurrency2":
								query = isOrdered ? ordered.ThenBy(o => o.PrimaryDefaultCurrency2) : query.OrderBy(o => o.PrimaryDefaultCurrency2);
								 break;
							case "PrimaryDefaultCurrency2_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PrimaryDefaultCurrency2) : query.OrderByDescending(o => o.PrimaryDefaultCurrency2);
                                break;
							case "IsPriceListForVendor":
								query = isOrdered ? ordered.ThenBy(o => o.IsPriceListForVendor) : query.OrderBy(o => o.IsPriceListForVendor);
								 break;
							case "IsPriceListForVendor_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsPriceListForVendor) : query.OrderByDescending(o => o.IsPriceListForVendor);
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

        public static IQueryable<tbl_WMS_PriceList> _pagingBuilder(IQueryable<tbl_WMS_PriceList> query, Dictionary<string, string> QueryStrings)
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






