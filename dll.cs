using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Common;
using ClassLibrary;

namespace DAL
{
   public class dlSubCategory
    {
     
       Database db = GetDatabase(AppDatabaseName);
       private static string AppDatabaseName;
       private static Database GetDatabase(string dbName)
       {
           try
           {
               Database db = null;
               if (dbName == null)
               {
                   return db = DatabaseFactory.CreateDatabase();
               }
               else
               {
                   return db = DatabaseFactory.CreateDatabase(dbName);
               }
               return db;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       static dlSubCategory()
       {
           AppDatabaseName = ConfigurationSettings.AppSettings["AppDatabaseName"];
       }


       public bool InsertSubcategory(SubCategoryCS scat)
       {
           try
           {

               DbCommand cmd = db.GetStoredProcCommand("spSubCategory");
               db.AddInParameter(cmd, "@CategoryID", DbType.String, scat.CatID);
               db.AddInParameter(cmd, "@SubCategory", DbType.String, scat.SubCategory);
               db.AddInParameter(cmd, "@Image", DbType.String, scat.Image);
               db.AddInParameter(cmd, "@Description", DbType.String, scat.Description);
               db.AddInParameter(cmd, "@Submitby", DbType.String, scat.Submitby);
               db.AddInParameter(cmd, "@Action", DbType.String, "Insert");
               int i = db.ExecuteNonQuery(cmd);

               if (i > 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }


       }


       public bool updateSubCategory(SubCategoryCS scat)
       {
           try
           {
               DbCommand cmd = db.GetStoredProcCommand("spSubCategory");
               db.AddInParameter(cmd, "@SubCatID", DbType.Int32, scat.SubCatID);

               db.AddInParameter(cmd, "@SortId", DbType.String, scat.SortID);
               db.AddInParameter(cmd, "@SubCategory", DbType.String, scat.SubCategory);
               db.AddInParameter(cmd, "@Image", DbType.String, scat.Image);
               db.AddInParameter(cmd, "@Description", DbType.String, scat.Description);

               db.AddInParameter(cmd, "@Action", DbType.String, "Update");
               int i = db.ExecuteNonQuery(cmd);
               if (i > 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }

           }
           catch (Exception ex)
           {

               throw ex;

           }
       }

       public void DelecteSubCategory(string SubCatID) 
       {
           try
           {
               DbCommand cmd = db.GetSqlStringCommand("Delete from SubCategory where SubCatID=@SubCatID");
               db.AddInParameter(cmd, "@SubCatID", DbType.String, SubCatID);
               db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public string getSubCategoryID(string SubCatName)
       {
           try
           {
               DbCommand cmd = db.GetSqlStringCommand("select SubCatID from SubCategory where SubCategory=@SubCategory");
               db.AddInParameter(cmd, "@SubCategory", DbType.String, SubCatName);
               object obj = db.ExecuteScalar(cmd);
               if (obj == null || System.DBNull.Value == obj)
               {
                   return "0";
               }
               else
               {
                   return obj.ToString();
               }
           }
           catch (Exception ex)
           {
                return ex.ToString();
           }
       }


       public string getCategoryID(string CatName)
       {
           try
           {
               DbCommand cmd = db.GetSqlStringCommand("select CatID from Categorytable where Category=@Category");
               db.AddInParameter(cmd, "@Category", DbType.String, CatName);
               object obj = db.ExecuteScalar(cmd);

               if (obj == null || System.DBNull.Value == obj)
               {
                   return "0";
               }
               else
               {
                   return obj.ToString();
               }
           }
           catch (Exception ex)
           {
              return ex.ToString();
           }


       }

       public DataSet getSubCategoryList(SubCategoryCS sc)
       {
           try
           {
               DbCommand cmd = db.GetStoredProcCommand("spSubCategory");
               db.AddInParameter(cmd, "@CategoryID", DbType.String, sc.CatID);
               db.AddInParameter(cmd, "@Action", DbType.String, "getSubCategoryList");
               DataSet ds = db.ExecuteDataSet(cmd);
               return ds;
           }
           catch (Exception ex)
           {

               throw ex;

           }


       }



       //public string SelectMaxForSubCat()
       //{
       //    DbCommand cmd = db.GetSqlStringCommand("select max(sortid) from SubCategory");
       //    string str = db.ExecuteScalar(cmd).ToString();


       //    if (string.IsNullOrEmpty(str) | str == "0")
       //    {
       //        return "0";
       //    }
       //    else
       //    {
       //        return str;
       //    }




       //}


       public bool chkSubCatSortid(string sortid) 
       {
           string str = null;

           DbCommand cmd = db.GetSqlStringCommand("Select subcatid from SubCategory where sortid=@sortid");
           db.AddInParameter(cmd, "@sortid", DbType.String, sortid);
           str = db.ExecuteScalar(cmd).ToString();
           //  str = SqlHelper.ExecuteScalar(System.Data.CommandType.Text, "select catid from CategoryTable where sortid=@sortid", new SqlParameter("@sortid", srtid));
           if (string.IsNullOrEmpty(str) | str == "0")
           {
               return true;
           }
           else
           {
               return false;
           }
       }


       public DataSet SelSubCatbysrtid(string sortid) 
       {
           DbCommand cmd = db.GetSqlStringCommand("select * from SubCategory where SortId >=@SortId order by sortid");
           db.AddInParameter(cmd, "@SortId", DbType.Decimal, sortid);
           return db.ExecuteDataSet(cmd);


       }

       public DataSet getSubCategoryNameList(string catid)  
       {
           DbCommand cmd = db.GetSqlStringCommand("select Distinct SubCategory,subcatid from SubCategory where categoryid=@catid and IsVisible=1 order by subcatid desc");
           db.AddInParameter(cmd, "@catid", DbType.String, catid);
           return db.ExecuteDataSet(cmd);
       }

       public string UpdateSortid(string sortid, string catid)
       {
           DbCommand cmd = db.GetSqlStringCommand("Update SubCategory set SortId =@SortId where SubCatID =@SubCatID");
           db.AddInParameter(cmd, "@SortId", DbType.String, sortid);
           db.AddInParameter(cmd, "@SubCatID", DbType.String, catid);

           return db.ExecuteNonQuery(cmd).ToString();




       }

       public string updSubCatVisibility(string IsVisible, string catid) 
       {


           DbCommand cmdupd = db.GetSqlStringCommand("UPDATE SubCategory SET IsVisible = @IsVisible  WHERE SubCatID = @SubCatID");
           db.AddInParameter(cmdupd, "@SubCatID", DbType.String, catid);
           db.AddInParameter(cmdupd, "@IsVisible", DbType.Boolean, IsVisible);

           try
           {
               return db.ExecuteNonQuery(cmdupd).ToString();
           }
           catch (Exception err)
           {

               return err.ToString();
           }

       }

       public DataSet getSubCatAndCatList(string CatID)
       {
           DbCommand cmd = db.GetSqlStringCommand("select * from catAndscatList  where categoryid=@CatID");//and subcatid=isnull(@ScatID,subcatid)
           db.AddInParameter(cmd, "@CatID", DbType.String, CatID);
         //  db.AddInParameter(cmd, "@subcatid", DbType.String, sc.SubCatID);
           return db.ExecuteDataSet(cmd);
       }

    }
}
