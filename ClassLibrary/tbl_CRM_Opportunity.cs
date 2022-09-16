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
    
    
    public partial class tbl_CRM_Opportunity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_CRM_Opportunity()
        {
            this.tbl_CRM_Activity = new HashSet<tbl_CRM_Activity>();
            this.tbl_CRM_Contract = new HashSet<tbl_CRM_Contract>();
            this.tbl_CRM_Quotation = new HashSet<tbl_CRM_Quotation>();
            this.tbl_PM_Task = new HashSet<tbl_PM_Task>();
        }
    
        public Nullable<int> IDLead { get; set; }
        public Nullable<int> IDContact { get; set; }
        public Nullable<int> IDSource { get; set; }
        public Nullable<int> IDStage { get; set; }
        public Nullable<int> IDType { get; set; }
        public Nullable<int> IDOwner { get; set; }
        public Nullable<int> IDCampaign { get; set; }
        public int IDBranch { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EventDate { get; set; }
        public Nullable<System.DateTime> PredictedClosingDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public decimal ExpectedRevenue { get; set; }
        public string NextStep { get; set; }
        public bool IsPrivate { get; set; }
        public Nullable<int> Probability { get; set; }
        public Nullable<int> TotalOpportunityQuantity { get; set; }
        public Nullable<int> Sort { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int NumberOfGuests { get; set; }
        public Nullable<int> RefID { get; set; }
        public Nullable<int> RefContactID { get; set; }
        public Nullable<int> RefSourceID { get; set; }
        public Nullable<int> RefOwnerID { get; set; }
        public string RefAccountCode { get; set; }
        //List 0:1
        public virtual tbl_BRA_Branch tbl_BRA_Branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_Activity> tbl_CRM_Activity { get; set; }
        //List 0:1
        public virtual tbl_CRM_Campaign tbl_CRM_Campaign { get; set; }
        //List 0:1
        public virtual tbl_CRM_Contact tbl_CRM_Contact { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_Contract> tbl_CRM_Contract { get; set; }
        //List 0:1
        public virtual tbl_CRM_Lead tbl_CRM_Lead { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CRM_Quotation> tbl_CRM_Quotation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PM_Task> tbl_PM_Task { get; set; }
    }
}


namespace DTOModel
{
	using System;
	public partial class DTO_CRM_Opportunity
	{
		public Nullable<int> IDLead { get; set; }
		public Nullable<int> IDContact { get; set; }
		public Nullable<int> IDSource { get; set; }
		public Nullable<int> IDStage { get; set; }
		public Nullable<int> IDType { get; set; }
		public Nullable<int> IDOwner { get; set; }
		public Nullable<int> IDCampaign { get; set; }
		public int IDBranch { get; set; }
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Remark { get; set; }
		public decimal Amount { get; set; }
		public System.DateTime StartDate { get; set; }
		public System.DateTime EventDate { get; set; }
		public Nullable<System.DateTime> PredictedClosingDate { get; set; }
		public Nullable<System.DateTime> ClosedDate { get; set; }
		public decimal ExpectedRevenue { get; set; }
		public string NextStep { get; set; }
		public bool IsPrivate { get; set; }
		public Nullable<int> Probability { get; set; }
		public Nullable<int> TotalOpportunityQuantity { get; set; }
		public Nullable<int> Sort { get; set; }
		public bool IsDisabled { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public string ModifiedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public System.DateTime ModifiedDate { get; set; }
		public int NumberOfGuests { get; set; }
		public Nullable<int> RefID { get; set; }
		public Nullable<int> RefContactID { get; set; }
		public Nullable<int> RefSourceID { get; set; }
		public Nullable<int> RefOwnerID { get; set; }
		public string RefAccountCode { get; set; }
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

    public static partial class BS_CRM_Opportunity 
    {
        public static dynamic getSearch(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch,StaffID, QueryStrings);
        }

		public static IQueryable<DTO_CRM_Opportunity> get(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
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
            package.Workbook.Properties.Title = "ART-DMS CRM_Opportunity";
            package.Workbook.Properties.Author = "hung.vu@codeart.vn";
            package.Workbook.Properties.Application = "ART-DMS";
            package.Workbook.Properties.Company = "A.R.T Distribution";

            ExcelWorkbook workBook = package.Workbook;
            if (workBook != null)
            {
                var ws = workBook.Worksheets.FirstOrDefault();
                var data = _queryBuilder(db, IDBranch, StaffID, QueryStrings).ToList();

                //var BRA_BranchList = BS_BRA_Branch.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var CRM_CampaignList = BS_CRM_Campaign.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var CRM_ContactList = BS_CRM_Contact.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                //var CRM_LeadList = BS_CRM_Lead.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                 

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

		public static DTO_CRM_Opportunity getAnItem(AppEntities db, int IDBranch, int StaffID, int id)
        {
            var dbResult = db.tbl_CRM_Opportunity.Find(id);

			//if (dbResult == null || (IDBranch != 0 && dbResult.IDBranch != IDBranch))
			//	return null; 
			//else 
				return toDTO(dbResult);
			
        }
		
		public static DTO_CRM_Opportunity getAnItemByCode(AppEntities db, int IDBranch, int StaffID, string code)
        {
            var dbResult = db.tbl_CRM_Opportunity
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
            var dbitem = db.tbl_CRM_Opportunity.Find(Id);
            
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
					errorLog.logMessage("put_CRM_Opportunity",e);
                    result = false;
                }
            }
            else
                if (QueryStrings.Any(d => d.Key == "ForceCreate"))
                    result =  post(db, IDBranch, StaffID, item, Username) != null;
                
            return result;
        }

        public static void patchDynamicToDB(dynamic item, tbl_CRM_Opportunity dbitem, string Username)
        {
            Type type = typeof(tbl_CRM_Opportunity);
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

        public static void patchDTOtoDBValue( DTO_CRM_Opportunity item, tbl_CRM_Opportunity dbitem)
        {
            if (item == null){
                dbitem = null;
                return;
            }
            							
			dbitem.IDLead = item.IDLead;							
			dbitem.IDContact = item.IDContact;							
			dbitem.IDSource = item.IDSource;							
			dbitem.IDStage = item.IDStage;							
			dbitem.IDType = item.IDType;							
			dbitem.IDOwner = item.IDOwner;							
			dbitem.IDCampaign = item.IDCampaign;							
			dbitem.IDBranch = item.IDBranch;							
			dbitem.Code = item.Code;							
			dbitem.Name = item.Name;							
			dbitem.Remark = item.Remark;							
			dbitem.Amount = item.Amount;							
			dbitem.StartDate = item.StartDate;							
			dbitem.EventDate = item.EventDate;							
			dbitem.PredictedClosingDate = item.PredictedClosingDate;							
			dbitem.ClosedDate = item.ClosedDate;							
			dbitem.ExpectedRevenue = item.ExpectedRevenue;							
			dbitem.NextStep = item.NextStep;							
			dbitem.IsPrivate = item.IsPrivate;							
			dbitem.Probability = item.Probability;							
			dbitem.TotalOpportunityQuantity = item.TotalOpportunityQuantity;							
			dbitem.Sort = item.Sort;							
			dbitem.IsDisabled = item.IsDisabled;							
			dbitem.IsDeleted = item.IsDeleted;							
			dbitem.NumberOfGuests = item.NumberOfGuests;							
			dbitem.RefID = item.RefID;							
			dbitem.RefContactID = item.RefContactID;							
			dbitem.RefSourceID = item.RefSourceID;							
			dbitem.RefOwnerID = item.RefOwnerID;							
			dbitem.RefAccountCode = item.RefAccountCode;        }

		public static DTO_CRM_Opportunity post(AppEntities db, int IDBranch, int StaffID, dynamic item, string Username)
        {
            tbl_CRM_Opportunity dbitem = new tbl_CRM_Opportunity();
            if (item != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                
				dbitem.CreatedBy = Username;
				dbitem.CreatedDate = DateTime.Now;				
                try
                {
					db.tbl_CRM_Opportunity.Add(dbitem);
                    db.SaveChanges();
				
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("post_CRM_Opportunity",e);
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
                Type type = Type.GetType("BaseBusiness.BS_CRM_Opportunity, ClassLibrary");
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
                        var target = new tbl_CRM_Opportunity();
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
                    
                    tbl_CRM_Opportunity dbitem = new tbl_CRM_Opportunity();
                    if (it.Id == null || string.IsNullOrEmpty(it.Id.Value) || it.Id == "0")
                    {
                        dbitem = new tbl_CRM_Opportunity();
                        dbitem.CreatedBy = Username;
				        dbitem.CreatedDate = DateTime.Now;
                                            }
                    else
                    {
                        dbitem = db.tbl_CRM_Opportunity.Find((int)it.Id);
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
                        
                        if (dbitem.Id == 0) db.tbl_CRM_Opportunity.Add(dbitem);
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
                    errorLog.logMessage("post_CRM_Opportunity",e);
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

            var dbitems = db.tbl_CRM_Opportunity.Where(d => IDs.Contains(d.Id));
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
                    BS_Version.update_Version(db, IDBranch, "DTO_CRM_Opportunity", updateDate, Username);
                }
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("delete_CRM_Opportunity",e);
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

			
            var dbitems = db.tbl_CRM_Opportunity.Where(d => IDs.Contains(d.Id));
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
                    BS_Version.update_Version(db, IDBranch, "DTO_CRM_Opportunity", updateDate, Username);
                }
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("disable_CRM_Opportunity",e);
                result = false;
            }
            
            return result;
        }

		
		//---//
		public static bool check_Exists(AppEntities db, int id)
		{
             
            return db.tbl_CRM_Opportunity.Any(e => e.Id == id && e.IsDeleted == false);
            
		}
		
		public static bool check_Exists(AppEntities db, string Code)
		{
             
            return db.tbl_CRM_Opportunity.Any(e => e.Code == Code && e.IsDeleted == false);
            
		}
		
		//---//
		public static IQueryable<DTO_CRM_Opportunity> toDTO(IQueryable<tbl_CRM_Opportunity> query)
        {
			return query
			.Select(s => new DTO_CRM_Opportunity(){							
				IDLead = s.IDLead,							
				IDContact = s.IDContact,							
				IDSource = s.IDSource,							
				IDStage = s.IDStage,							
				IDType = s.IDType,							
				IDOwner = s.IDOwner,							
				IDCampaign = s.IDCampaign,							
				IDBranch = s.IDBranch,							
				Id = s.Id,							
				Code = s.Code,							
				Name = s.Name,							
				Remark = s.Remark,							
				Amount = s.Amount,							
				StartDate = s.StartDate,							
				EventDate = s.EventDate,							
				PredictedClosingDate = s.PredictedClosingDate,							
				ClosedDate = s.ClosedDate,							
				ExpectedRevenue = s.ExpectedRevenue,							
				NextStep = s.NextStep,							
				IsPrivate = s.IsPrivate,							
				Probability = s.Probability,							
				TotalOpportunityQuantity = s.TotalOpportunityQuantity,							
				Sort = s.Sort,							
				IsDisabled = s.IsDisabled,							
				IsDeleted = s.IsDeleted,							
				CreatedBy = s.CreatedBy,							
				ModifiedBy = s.ModifiedBy,							
				CreatedDate = s.CreatedDate,							
				ModifiedDate = s.ModifiedDate,							
				NumberOfGuests = s.NumberOfGuests,							
				RefID = s.RefID,							
				RefContactID = s.RefContactID,							
				RefSourceID = s.RefSourceID,							
				RefOwnerID = s.RefOwnerID,							
				RefAccountCode = s.RefAccountCode,					
			});//.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                              
        }

		public static DTO_CRM_Opportunity toDTO(tbl_CRM_Opportunity dbResult)
        {
			if (dbResult != null)
			{
				return new DTO_CRM_Opportunity()
				{							
					IDLead = dbResult.IDLead,							
					IDContact = dbResult.IDContact,							
					IDSource = dbResult.IDSource,							
					IDStage = dbResult.IDStage,							
					IDType = dbResult.IDType,							
					IDOwner = dbResult.IDOwner,							
					IDCampaign = dbResult.IDCampaign,							
					IDBranch = dbResult.IDBranch,							
					Id = dbResult.Id,							
					Code = dbResult.Code,							
					Name = dbResult.Name,							
					Remark = dbResult.Remark,							
					Amount = dbResult.Amount,							
					StartDate = dbResult.StartDate,							
					EventDate = dbResult.EventDate,							
					PredictedClosingDate = dbResult.PredictedClosingDate,							
					ClosedDate = dbResult.ClosedDate,							
					ExpectedRevenue = dbResult.ExpectedRevenue,							
					NextStep = dbResult.NextStep,							
					IsPrivate = dbResult.IsPrivate,							
					Probability = dbResult.Probability,							
					TotalOpportunityQuantity = dbResult.TotalOpportunityQuantity,							
					Sort = dbResult.Sort,							
					IsDisabled = dbResult.IsDisabled,							
					IsDeleted = dbResult.IsDeleted,							
					CreatedBy = dbResult.CreatedBy,							
					ModifiedBy = dbResult.ModifiedBy,							
					CreatedDate = dbResult.CreatedDate,							
					ModifiedDate = dbResult.ModifiedDate,							
					NumberOfGuests = dbResult.NumberOfGuests,							
					RefID = dbResult.RefID,							
					RefContactID = dbResult.RefContactID,							
					RefSourceID = dbResult.RefSourceID,							
					RefOwnerID = dbResult.RefOwnerID,							
					RefAccountCode = dbResult.RefAccountCode,
				};
			}
			else
				return null; 
        }

		public static IQueryable<tbl_CRM_Opportunity> _queryBuilder(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            
			IQueryable<tbl_CRM_Opportunity> query = db.tbl_CRM_Opportunity.AsNoTracking();//.Where(d => d.IsDeleted == false);
			

			//Query keyword
			if (QueryStrings.Any(d => d.Key == "Keyword") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value;
                query = query.Where(d=>d.Name.Contains(keyword) || d.Code.Contains(keyword));
            }



			//Query IDLead (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDLead"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDLead").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDLead));
////                    query = query.Where(d => IDs.Contains(d.IDLead));
//                    
            }

			//Query IDContact (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDContact"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDContact").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDContact));
////                    query = query.Where(d => IDs.Contains(d.IDContact));
//                    
            }

			//Query IDSource (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDSource"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDSource").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDSource));
////                    query = query.Where(d => IDs.Contains(d.IDSource));
//                    
            }

			//Query IDStage (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDStage"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDStage").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDStage));
////                    query = query.Where(d => IDs.Contains(d.IDStage));
//                    
            }

			//Query IDType (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDType"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDType").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDType));
////                    query = query.Where(d => IDs.Contains(d.IDType));
//                    
            }

			//Query IDOwner (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDOwner"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDOwner").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDOwner));
////                    query = query.Where(d => IDs.Contains(d.IDOwner));
//                    
            }

			//Query IDCampaign (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDCampaign"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDCampaign").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDCampaign));
////                    query = query.Where(d => IDs.Contains(d.IDCampaign));
//                    
            }

			//Query IDBranch (int)
			if (QueryStrings.Any(d => d.Key == "IDBranch"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDBranch").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int> IDs = new List<int>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDBranch));
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
            

			//Query Amount (decimal)
			if (QueryStrings.Any(d => d.Key == "AmountFrom") && QueryStrings.Any(d => d.Key == "AmountTo"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "AmountFrom").Value, out decimal fromVal) && decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "AmountTo").Value, out decimal toVal))
                    query = query.Where(d => fromVal <= d.Amount && d.Amount <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "Amount"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "Amount").Value, out decimal val))
                    query = query.Where(d => val == d.Amount);


			//Query StartDate (System.DateTime)
			if (QueryStrings.Any(d => d.Key == "StartDateFrom") && QueryStrings.Any(d => d.Key == "StartDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "StartDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "StartDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.StartDate && d.StartDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "StartDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "StartDate").Value, out DateTime val))
                    query = query.Where(d => d.StartDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.StartDate));


			//Query EventDate (System.DateTime)
			if (QueryStrings.Any(d => d.Key == "EventDateFrom") && QueryStrings.Any(d => d.Key == "EventDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "EventDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "EventDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.EventDate && d.EventDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "EventDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "EventDate").Value, out DateTime val))
                    query = query.Where(d => d.EventDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.EventDate));


			//Query PredictedClosingDate (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "PredictedClosingDateFrom") && QueryStrings.Any(d => d.Key == "PredictedClosingDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "PredictedClosingDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "PredictedClosingDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.PredictedClosingDate && d.PredictedClosingDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "PredictedClosingDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "PredictedClosingDate").Value, out DateTime val))
                    query = query.Where(d => d.PredictedClosingDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.PredictedClosingDate));


			//Query ClosedDate (Nullable<System.DateTime>)
			if (QueryStrings.Any(d => d.Key == "ClosedDateFrom") && QueryStrings.Any(d => d.Key == "ClosedDateTo"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ClosedDateFrom").Value, out DateTime fromDate) && DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ClosedDateTo").Value, out DateTime toDate))
                    query = query.Where(d => fromDate <= d.ClosedDate && d.ClosedDate <= toDate);

            if (QueryStrings.Any(d => d.Key == "ClosedDate"))
                if (DateTime.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ClosedDate").Value, out DateTime val))
                    query = query.Where(d => d.ClosedDate != null && DbFunctions.TruncateTime(val) == DbFunctions.TruncateTime(d.ClosedDate));


			//Query ExpectedRevenue (decimal)
			if (QueryStrings.Any(d => d.Key == "ExpectedRevenueFrom") && QueryStrings.Any(d => d.Key == "ExpectedRevenueTo"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ExpectedRevenueFrom").Value, out decimal fromVal) && decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ExpectedRevenueTo").Value, out decimal toVal))
                    query = query.Where(d => fromVal <= d.ExpectedRevenue && d.ExpectedRevenue <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "ExpectedRevenue"))
                if (decimal.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ExpectedRevenue").Value, out decimal val))
                    query = query.Where(d => val == d.ExpectedRevenue);


			//Query NextStep (string)
			if (QueryStrings.Any(d => d.Key == "NextStep_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "NextStep_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "NextStep_eq").Value;
                query = query.Where(d=>d.NextStep == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "NextStep") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "NextStep").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "NextStep").Value;
                query = query.Where(d=>d.NextStep.Contains(keyword));
            }
            

			//Query IsPrivate (bool)
			if (QueryStrings.Any(d => d.Key == "IsPrivate"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "IsPrivate").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.IsPrivate);
            }

			//Query Probability (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "ProbabilityFrom") && QueryStrings.Any(d => d.Key == "ProbabilityTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ProbabilityFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "ProbabilityTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.Probability && d.Probability <= toVal);

			//Query TotalOpportunityQuantity (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "TotalOpportunityQuantityFrom") && QueryStrings.Any(d => d.Key == "TotalOpportunityQuantityTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "TotalOpportunityQuantityFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "TotalOpportunityQuantityTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.TotalOpportunityQuantity && d.TotalOpportunityQuantity <= toVal);

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


			//Query NumberOfGuests (int)
			if (QueryStrings.Any(d => d.Key == "NumberOfGuestsFrom") && QueryStrings.Any(d => d.Key == "NumberOfGuestsTo"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuestsFrom").Value, out int fromVal) && int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuestsTo").Value, out int toVal))
                    query = query.Where(d => fromVal <= d.NumberOfGuests && d.NumberOfGuests <= toVal);
            
            if (QueryStrings.Any(d => d.Key == "NumberOfGuests"))
                if (int.TryParse(QueryStrings.FirstOrDefault(d => d.Key == "NumberOfGuests").Value, out int val))
                    query = query.Where(d => val == d.NumberOfGuests);


			//Query RefID (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "RefID"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "RefID").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.RefID));
////                    query = query.Where(d => IDs.Contains(d.RefID));
//                    
            }

			//Query RefContactID (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "RefContactID"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "RefContactID").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.RefContactID));
////                    query = query.Where(d => IDs.Contains(d.RefContactID));
//                    
            }

			//Query RefSourceID (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "RefSourceID"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "RefSourceID").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.RefSourceID));
////                    query = query.Where(d => IDs.Contains(d.RefSourceID));
//                    
            }

			//Query RefOwnerID (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "RefOwnerID"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "RefOwnerID").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.RefOwnerID));
////                    query = query.Where(d => IDs.Contains(d.RefOwnerID));
//                    
            }

			//Query RefAccountCode (string)
			if (QueryStrings.Any(d => d.Key == "RefAccountCode_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "RefAccountCode_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "RefAccountCode_eq").Value;
                query = query.Where(d=>d.RefAccountCode == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "RefAccountCode") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "RefAccountCode").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "RefAccountCode").Value;
                query = query.Where(d=>d.RefAccountCode.Contains(keyword));
            }
            
            return query;
        }
		
        public static IQueryable<tbl_CRM_Opportunity> _sortBuilder(IQueryable<tbl_CRM_Opportunity> query, Dictionary<string, string> QueryStrings)
        {
            if (QueryStrings.Any(d => d.Key == "SortBy"))
            {
                var sorts = QueryStrings.FirstOrDefault(d => d.Key == "SortBy").Value.Replace("[", "").Replace("]", "").Split(',');
                
                foreach (var item in sorts)
                    if (!string.IsNullOrEmpty(item))
                    {
                        var ordered = query as IOrderedQueryable<tbl_CRM_Opportunity>;
                        bool isOrdered = ordered.ToString().IndexOf("ORDER BY") !=-1;

                        switch (item)
                        {
							case "IDLead":
								query = isOrdered ? ordered.ThenBy(o => o.IDLead) : query.OrderBy(o => o.IDLead);
								 break;
							case "IDLead_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDLead) : query.OrderByDescending(o => o.IDLead);
                                break;
							case "IDContact":
								query = isOrdered ? ordered.ThenBy(o => o.IDContact) : query.OrderBy(o => o.IDContact);
								 break;
							case "IDContact_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDContact) : query.OrderByDescending(o => o.IDContact);
                                break;
							case "IDSource":
								query = isOrdered ? ordered.ThenBy(o => o.IDSource) : query.OrderBy(o => o.IDSource);
								 break;
							case "IDSource_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDSource) : query.OrderByDescending(o => o.IDSource);
                                break;
							case "IDStage":
								query = isOrdered ? ordered.ThenBy(o => o.IDStage) : query.OrderBy(o => o.IDStage);
								 break;
							case "IDStage_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDStage) : query.OrderByDescending(o => o.IDStage);
                                break;
							case "IDType":
								query = isOrdered ? ordered.ThenBy(o => o.IDType) : query.OrderBy(o => o.IDType);
								 break;
							case "IDType_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDType) : query.OrderByDescending(o => o.IDType);
                                break;
							case "IDOwner":
								query = isOrdered ? ordered.ThenBy(o => o.IDOwner) : query.OrderBy(o => o.IDOwner);
								 break;
							case "IDOwner_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDOwner) : query.OrderByDescending(o => o.IDOwner);
                                break;
							case "IDCampaign":
								query = isOrdered ? ordered.ThenBy(o => o.IDCampaign) : query.OrderBy(o => o.IDCampaign);
								 break;
							case "IDCampaign_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDCampaign) : query.OrderByDescending(o => o.IDCampaign);
                                break;
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
							case "Amount":
								query = isOrdered ? ordered.ThenBy(o => o.Amount) : query.OrderBy(o => o.Amount);
								 break;
							case "Amount_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Amount) : query.OrderByDescending(o => o.Amount);
                                break;
							case "StartDate":
								query = isOrdered ? ordered.ThenBy(o => o.StartDate) : query.OrderBy(o => o.StartDate);
								 break;
							case "StartDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.StartDate) : query.OrderByDescending(o => o.StartDate);
                                break;
							case "EventDate":
								query = isOrdered ? ordered.ThenBy(o => o.EventDate) : query.OrderBy(o => o.EventDate);
								 break;
							case "EventDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.EventDate) : query.OrderByDescending(o => o.EventDate);
                                break;
							case "PredictedClosingDate":
								query = isOrdered ? ordered.ThenBy(o => o.PredictedClosingDate) : query.OrderBy(o => o.PredictedClosingDate);
								 break;
							case "PredictedClosingDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.PredictedClosingDate) : query.OrderByDescending(o => o.PredictedClosingDate);
                                break;
							case "ClosedDate":
								query = isOrdered ? ordered.ThenBy(o => o.ClosedDate) : query.OrderBy(o => o.ClosedDate);
								 break;
							case "ClosedDate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ClosedDate) : query.OrderByDescending(o => o.ClosedDate);
                                break;
							case "ExpectedRevenue":
								query = isOrdered ? ordered.ThenBy(o => o.ExpectedRevenue) : query.OrderBy(o => o.ExpectedRevenue);
								 break;
							case "ExpectedRevenue_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ExpectedRevenue) : query.OrderByDescending(o => o.ExpectedRevenue);
                                break;
							case "NextStep":
								query = isOrdered ? ordered.ThenBy(o => o.NextStep) : query.OrderBy(o => o.NextStep);
								 break;
							case "NextStep_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.NextStep) : query.OrderByDescending(o => o.NextStep);
                                break;
							case "IsPrivate":
								query = isOrdered ? ordered.ThenBy(o => o.IsPrivate) : query.OrderBy(o => o.IsPrivate);
								 break;
							case "IsPrivate_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsPrivate) : query.OrderByDescending(o => o.IsPrivate);
                                break;
							case "Probability":
								query = isOrdered ? ordered.ThenBy(o => o.Probability) : query.OrderBy(o => o.Probability);
								 break;
							case "Probability_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Probability) : query.OrderByDescending(o => o.Probability);
                                break;
							case "TotalOpportunityQuantity":
								query = isOrdered ? ordered.ThenBy(o => o.TotalOpportunityQuantity) : query.OrderBy(o => o.TotalOpportunityQuantity);
								 break;
							case "TotalOpportunityQuantity_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.TotalOpportunityQuantity) : query.OrderByDescending(o => o.TotalOpportunityQuantity);
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
							case "NumberOfGuests":
								query = isOrdered ? ordered.ThenBy(o => o.NumberOfGuests) : query.OrderBy(o => o.NumberOfGuests);
								 break;
							case "NumberOfGuests_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.NumberOfGuests) : query.OrderByDescending(o => o.NumberOfGuests);
                                break;
							case "RefID":
								query = isOrdered ? ordered.ThenBy(o => o.RefID) : query.OrderBy(o => o.RefID);
								 break;
							case "RefID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RefID) : query.OrderByDescending(o => o.RefID);
                                break;
							case "RefContactID":
								query = isOrdered ? ordered.ThenBy(o => o.RefContactID) : query.OrderBy(o => o.RefContactID);
								 break;
							case "RefContactID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RefContactID) : query.OrderByDescending(o => o.RefContactID);
                                break;
							case "RefSourceID":
								query = isOrdered ? ordered.ThenBy(o => o.RefSourceID) : query.OrderBy(o => o.RefSourceID);
								 break;
							case "RefSourceID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RefSourceID) : query.OrderByDescending(o => o.RefSourceID);
                                break;
							case "RefOwnerID":
								query = isOrdered ? ordered.ThenBy(o => o.RefOwnerID) : query.OrderBy(o => o.RefOwnerID);
								 break;
							case "RefOwnerID_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RefOwnerID) : query.OrderByDescending(o => o.RefOwnerID);
                                break;
							case "RefAccountCode":
								query = isOrdered ? ordered.ThenBy(o => o.RefAccountCode) : query.OrderBy(o => o.RefAccountCode);
								 break;
							case "RefAccountCode_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.RefAccountCode) : query.OrderByDescending(o => o.RefAccountCode);
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

        public static IQueryable<tbl_CRM_Opportunity> _pagingBuilder(IQueryable<tbl_CRM_Opportunity> query, Dictionary<string, string> QueryStrings)
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






