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
    
    
    public partial class tbl_WEB_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_WEB_Category()
        {
            this.tbl_WEB_Category1 = new HashSet<tbl_WEB_Category>();
            this.tbl_WEB_Content = new HashSet<tbl_WEB_Content>();
        }
    
        public Nullable<int> IDParent { get; set; }
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
        public string ThumbnailImage { get; set; }
        public bool Duyet { get; set; }
        public string NgonNgu { get; set; }
        public string Summary { get; set; }
        public string URL { get; set; }
        public string SEO1 { get; set; }
        public string SEO2 { get; set; }
        public bool IsBuildIn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_WEB_Category> tbl_WEB_Category1 { get; set; }
        //List 0:1
        public virtual tbl_WEB_Category tbl_WEB_Category2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_WEB_Content> tbl_WEB_Content { get; set; }
    }
}


namespace DTOModel
{
	using System;
	public partial class DTO_WEB_Category
	{
		public Nullable<int> IDParent { get; set; }
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
		public string ThumbnailImage { get; set; }
		public bool Duyet { get; set; }
		public string NgonNgu { get; set; }
		public string Summary { get; set; }
		public string URL { get; set; }
		public string SEO1 { get; set; }
		public string SEO2 { get; set; }
		public bool IsBuildIn { get; set; }
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

    public static partial class BS_WEB_Category 
    {
        public static dynamic getSearch(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch,StaffID, QueryStrings);
        }

		public static IQueryable<DTO_WEB_Category> get(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
			var query = _queryBuilder(db, IDBranch, StaffID, QueryStrings);

            if (QueryStrings.Any(d => d.Key == "AllChildren")  || QueryStrings.Any(d => d.Key == "AllParent"))
            {

                List<DTO_WEB_Category> allItems = db.tbl_WEB_Category.Where(d => d.IsDeleted == false).Select(s => new DTO_WEB_Category()
                {
                    Id = s.Id,
                    IDParent = s.IDParent
                }).ToList();

                List<int> Ids = new List<int>();

                foreach (var item in query)
                {
                    Ids.Add(item.Id);
                    if (QueryStrings.Any(d => d.Key == "AllParent")){
                        Ids.AddRange(findParent(allItems, item.Id));
                    }
                    if (QueryStrings.Any(d => d.Key == "AllChildren")){
                        Ids.AddRange(findChildrent(allItems, item.Id).Select(s=>s));
                    }
                    
                    query = db.tbl_WEB_Category.Where(d => Ids.Contains(d.Id));
                }

            }

            query = _sortBuilder(query, QueryStrings);
            query = _pagingBuilder(query, QueryStrings);

			return toDTO(query);
        }

        public static List<int> findParent(List<DTO_WEB_Category> allItems, int Id)
        {

            var Ids = new List<int>();

            var item = allItems.FirstOrDefault(d => d.Id == Id);

            if (item != null && item.IDParent.HasValue)
            {
                Ids.Add(item.IDParent.Value);
           
                Ids.AddRange(findParent(allItems, item.IDParent.Value));
            }

            return Ids;
        }

        public static List<int> findChildrent(List<DTO_WEB_Category> allItems, int Id)
        {
            var Ids = new List<int>();
            var items = allItems.Where(d => d.IDParent == Id).Select(s => s.Id).ToList();
            Ids.AddRange(items);

            foreach (var item in items)
            {
                Ids.AddRange(findChildrent(allItems, item));
            }

            return Ids;
        }

        public static List<ItemModel> getValidateData(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            return get(db, IDBranch, StaffID, QueryStrings).Select(s => new ItemModel { 
                Id = s.Id,  Code = s.Code,  Name = s.Name, 
            }).ToList();
        }

        public static string export(ExcelPackage package, AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            package.Workbook.Properties.Title = "ART-DMS WEB_Category";
            package.Workbook.Properties.Author = "hung.vu@codeart.vn";
            package.Workbook.Properties.Application = "ART-DMS";
            package.Workbook.Properties.Company = "A.R.T Distribution";

            ExcelWorkbook workBook = package.Workbook;
            if (workBook != null)
            {
                var ws = workBook.Worksheets.FirstOrDefault();
                var data = _queryBuilder(db, IDBranch, StaffID, QueryStrings).ToList();

                //var WEB_Category2List = BS_WEB_Category.get(db,IDBranch, StaffID,QueryStrings).Select(s => new ItemModel { Id = s.Id, Name = s.Name });
                 

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

		public static DTO_WEB_Category getAnItem(AppEntities db, int IDBranch, int StaffID, int id)
        {
            var dbResult = db.tbl_WEB_Category.Find(id);

			return toDTO(dbResult);
			
        }
		
		public static DTO_WEB_Category getAnItemByCode(AppEntities db, int IDBranch, int StaffID, string code)
        {
            var dbResult = db.tbl_WEB_Category
			.FirstOrDefault(d => d.IsDeleted == false && d.Code == code );

			return toDTO(dbResult);
			
        }

		public static bool put(AppEntities db, int IDBranch, int StaffID, int Id, dynamic item, string Username, Dictionary<string, string> QueryStrings)
        {
            bool result = false;
            var dbitem = db.tbl_WEB_Category.Find(Id);
            
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
					errorLog.logMessage("put_WEB_Category",e);
                    result = false;
                }
            }
            else
                if (QueryStrings.Any(d => d.Key == "ForceCreate"))
                    result =  post(db, IDBranch, StaffID, item, Username) != null;
                
            return result;
        }

        public static void patchDynamicToDB(dynamic item, tbl_WEB_Category dbitem, string Username)
        {
            Type type = typeof(tbl_WEB_Category);
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

        public static void patchDTOtoDBValue( DTO_WEB_Category item, tbl_WEB_Category dbitem)
        {
            if (item == null){
                dbitem = null;
                return;
            }
            							
			dbitem.IDParent = item.IDParent;							
			dbitem.Code = item.Code;							
			dbitem.Name = item.Name;							
			dbitem.Remark = item.Remark;							
			dbitem.Sort = item.Sort;							
			dbitem.IsDisabled = item.IsDisabled;							
			dbitem.IsDeleted = item.IsDeleted;							
			dbitem.ThumbnailImage = item.ThumbnailImage;							
			dbitem.Duyet = item.Duyet;							
			dbitem.NgonNgu = item.NgonNgu;							
			dbitem.Summary = item.Summary;							
			dbitem.URL = item.URL;							
			dbitem.SEO1 = item.SEO1;							
			dbitem.SEO2 = item.SEO2;							
			dbitem.IsBuildIn = item.IsBuildIn;        }

		public static DTO_WEB_Category post(AppEntities db, int IDBranch, int StaffID, dynamic item, string Username)
        {
            tbl_WEB_Category dbitem = new tbl_WEB_Category();
            if (item != null)
            {
                patchDynamicToDB(item, dbitem, Username);
                
				dbitem.CreatedBy = Username;
				dbitem.CreatedDate = DateTime.Now;				
                try
                {
					db.tbl_WEB_Category.Add(dbitem);
                    db.SaveChanges();
				
                }
                catch (DbEntityValidationException e)
                {
					errorLog.logMessage("post_WEB_Category",e);
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
                Type type = Type.GetType("BaseBusiness.BS_WEB_Category, ClassLibrary");
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
                        var target = new tbl_WEB_Category();
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
                    
                    tbl_WEB_Category dbitem = new tbl_WEB_Category();
                    if (it.Id == null || string.IsNullOrEmpty(it.Id.Value) || it.Id == "0")
                    {
                        dbitem = new tbl_WEB_Category();
                        dbitem.CreatedBy = Username;
				        dbitem.CreatedDate = DateTime.Now;
                                            }
                    else
                    {
                        dbitem = db.tbl_WEB_Category.Find((int)it.Id);
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
                        
                        if (dbitem.Id == 0) db.tbl_WEB_Category.Add(dbitem);
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
                    errorLog.logMessage("post_WEB_Category",e);
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

            var dbitems = db.tbl_WEB_Category.Where(d => IDs.Contains(d.Id));
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
			
                BS_Version.update_Version(db, null, "DTO_WEB_Category", updateDate, Username);
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("delete_WEB_Category",e);
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

			
            var dbitems = db.tbl_WEB_Category.Where(d => IDs.Contains(d.Id));
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
			
                BS_Version.update_Version(db, null, "DTO_WEB_Category", updateDate, Username);
			
            }
            catch (DbEntityValidationException e)
            {
				errorLog.logMessage("disable_WEB_Category",e);
                result = false;
            }
            
            return result;
        }

		
		//---//
		public static bool check_Exists(AppEntities db, int id)
		{
             
            return db.tbl_WEB_Category.Any(e => e.Id == id && e.IsDeleted == false);
            
		}
		
		public static bool check_Exists(AppEntities db, string Code)
		{
             
            return db.tbl_WEB_Category.Any(e => e.Code == Code && e.IsDeleted == false);
            
		}
		
		//---//
		public static IQueryable<DTO_WEB_Category> toDTO(IQueryable<tbl_WEB_Category> query)
        {
			return query
			.Select(s => new DTO_WEB_Category(){							
				IDParent = s.IDParent,							
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
				ThumbnailImage = s.ThumbnailImage,							
				Duyet = s.Duyet,							
				NgonNgu = s.NgonNgu,							
				Summary = s.Summary,							
				URL = s.URL,							
				SEO1 = s.SEO1,							
				SEO2 = s.SEO2,							
				IsBuildIn = s.IsBuildIn,					
			});//.OrderBy(o => o.Sort == null).ThenBy(u => u.Sort);
                              
        }

		public static DTO_WEB_Category toDTO(tbl_WEB_Category dbResult)
        {
			if (dbResult != null)
			{
				return new DTO_WEB_Category()
				{							
					IDParent = dbResult.IDParent,							
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
					ThumbnailImage = dbResult.ThumbnailImage,							
					Duyet = dbResult.Duyet,							
					NgonNgu = dbResult.NgonNgu,							
					Summary = dbResult.Summary,							
					URL = dbResult.URL,							
					SEO1 = dbResult.SEO1,							
					SEO2 = dbResult.SEO2,							
					IsBuildIn = dbResult.IsBuildIn,
				};
			}
			else
				return null; 
        }

		public static IQueryable<tbl_WEB_Category> _queryBuilder(AppEntities db, int IDBranch, int StaffID, Dictionary<string, string> QueryStrings)
        {
            
			IQueryable<tbl_WEB_Category> query = db.tbl_WEB_Category.AsNoTracking();//.Where(d => d.IsDeleted == false );
			

			//Query keyword
			if (QueryStrings.Any(d => d.Key == "Keyword") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Keyword").Value;
                query = query.Where(d=>d.Name.Contains(keyword) || d.Code.Contains(keyword));
            }



			//Query IDParent (Nullable<int>)
			if (QueryStrings.Any(d => d.Key == "IDParent"))
            {
                var IDList = QueryStrings.FirstOrDefault(d => d.Key == "IDParent").Value.Replace("[", "").Replace("]", "").Split(',');
                List<int?> IDs = new List<int?>();
                foreach (var item in IDList)
                    if (int.TryParse(item, out int i))
                        IDs.Add(i);
					else if (item == "null")
						IDs.Add(null);
                if (IDs.Count > 0)
                    query = query.Where(d => IDs.Contains(d.IDParent));
////                    query = query.Where(d => IDs.Contains(d.IDParent));
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


			//Query ThumbnailImage (string)
			if (QueryStrings.Any(d => d.Key == "ThumbnailImage_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ThumbnailImage_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ThumbnailImage_eq").Value;
                query = query.Where(d=>d.ThumbnailImage == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "ThumbnailImage") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "ThumbnailImage").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "ThumbnailImage").Value;
                query = query.Where(d=>d.ThumbnailImage.Contains(keyword));
            }
            

			//Query Duyet (bool)
			if (QueryStrings.Any(d => d.Key == "Duyet"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "Duyet").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.Duyet);
            }

			//Query NgonNgu (string)
			if (QueryStrings.Any(d => d.Key == "NgonNgu_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "NgonNgu_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "NgonNgu_eq").Value;
                query = query.Where(d=>d.NgonNgu == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "NgonNgu") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "NgonNgu").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "NgonNgu").Value;
                query = query.Where(d=>d.NgonNgu.Contains(keyword));
            }
            

			//Query Summary (string)
			if (QueryStrings.Any(d => d.Key == "Summary_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Summary_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Summary_eq").Value;
                query = query.Where(d=>d.Summary == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "Summary") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "Summary").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "Summary").Value;
                query = query.Where(d=>d.Summary.Contains(keyword));
            }
            

			//Query URL (string)
			if (QueryStrings.Any(d => d.Key == "URL_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "URL_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "URL_eq").Value;
                query = query.Where(d=>d.URL == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "URL") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "URL").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "URL").Value;
                query = query.Where(d=>d.URL.Contains(keyword));
            }
            

			//Query SEO1 (string)
			if (QueryStrings.Any(d => d.Key == "SEO1_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "SEO1_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "SEO1_eq").Value;
                query = query.Where(d=>d.SEO1 == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "SEO1") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "SEO1").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "SEO1").Value;
                query = query.Where(d=>d.SEO1.Contains(keyword));
            }
            

			//Query SEO2 (string)
			if (QueryStrings.Any(d => d.Key == "SEO2_eq") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "SEO2_eq").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "SEO2_eq").Value;
                query = query.Where(d=>d.SEO2 == keyword);
            }
            if (QueryStrings.Any(d => d.Key == "SEO2") && !string.IsNullOrEmpty(QueryStrings.FirstOrDefault(d => d.Key == "SEO2").Value))
            {
                var keyword = QueryStrings.FirstOrDefault(d => d.Key == "SEO2").Value;
                query = query.Where(d=>d.SEO2.Contains(keyword));
            }
            

			//Query IsBuildIn (bool)
			if (QueryStrings.Any(d => d.Key == "IsBuildIn"))
            {
                var qValue = QueryStrings.FirstOrDefault(d => d.Key == "IsBuildIn").Value;
                if (bool.TryParse(qValue, out bool qBoolValue))
                    query = query.Where(d => qBoolValue == d.IsBuildIn);
            }
            return query;
        }
		
        public static IQueryable<tbl_WEB_Category> _sortBuilder(IQueryable<tbl_WEB_Category> query, Dictionary<string, string> QueryStrings)
        {
            if (QueryStrings.Any(d => d.Key == "SortBy"))
            {
                var sorts = QueryStrings.FirstOrDefault(d => d.Key == "SortBy").Value.Replace("[", "").Replace("]", "").Split(',');
                
                foreach (var item in sorts)
                    if (!string.IsNullOrEmpty(item))
                    {
                        var ordered = query as IOrderedQueryable<tbl_WEB_Category>;
                        bool isOrdered = ordered.ToString().IndexOf("ORDER BY") !=-1;

                        switch (item)
                        {
							case "IDParent":
								query = isOrdered ? ordered.ThenBy(o => o.IDParent) : query.OrderBy(o => o.IDParent);
								 break;
							case "IDParent_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IDParent) : query.OrderByDescending(o => o.IDParent);
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
							case "ThumbnailImage":
								query = isOrdered ? ordered.ThenBy(o => o.ThumbnailImage) : query.OrderBy(o => o.ThumbnailImage);
								 break;
							case "ThumbnailImage_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.ThumbnailImage) : query.OrderByDescending(o => o.ThumbnailImage);
                                break;
							case "Duyet":
								query = isOrdered ? ordered.ThenBy(o => o.Duyet) : query.OrderBy(o => o.Duyet);
								 break;
							case "Duyet_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Duyet) : query.OrderByDescending(o => o.Duyet);
                                break;
							case "NgonNgu":
								query = isOrdered ? ordered.ThenBy(o => o.NgonNgu) : query.OrderBy(o => o.NgonNgu);
								 break;
							case "NgonNgu_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.NgonNgu) : query.OrderByDescending(o => o.NgonNgu);
                                break;
							case "Summary":
								query = isOrdered ? ordered.ThenBy(o => o.Summary) : query.OrderBy(o => o.Summary);
								 break;
							case "Summary_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.Summary) : query.OrderByDescending(o => o.Summary);
                                break;
							case "URL":
								query = isOrdered ? ordered.ThenBy(o => o.URL) : query.OrderBy(o => o.URL);
								 break;
							case "URL_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.URL) : query.OrderByDescending(o => o.URL);
                                break;
							case "SEO1":
								query = isOrdered ? ordered.ThenBy(o => o.SEO1) : query.OrderBy(o => o.SEO1);
								 break;
							case "SEO1_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.SEO1) : query.OrderByDescending(o => o.SEO1);
                                break;
							case "SEO2":
								query = isOrdered ? ordered.ThenBy(o => o.SEO2) : query.OrderBy(o => o.SEO2);
								 break;
							case "SEO2_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.SEO2) : query.OrderByDescending(o => o.SEO2);
                                break;
							case "IsBuildIn":
								query = isOrdered ? ordered.ThenBy(o => o.IsBuildIn) : query.OrderBy(o => o.IsBuildIn);
								 break;
							case "IsBuildIn_desc":
                                query = isOrdered ? ordered.ThenByDescending(o => o.IsBuildIn) : query.OrderByDescending(o => o.IsBuildIn);
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

        public static IQueryable<tbl_WEB_Category> _pagingBuilder(IQueryable<tbl_WEB_Category> query, Dictionary<string, string> QueryStrings)
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





