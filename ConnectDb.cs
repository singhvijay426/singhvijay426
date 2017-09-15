using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL
{

  public class ConnectDb
    {
      private static string AppDatabaseName;

      //
      // Code for GetDatabase
      // 

        public static Database GetDatabase()
        {
            try
            {
                Database db = null;
                if (AppDatabaseName == null)
                {
                    return db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    return db = DatabaseFactory.CreateDatabase(AppDatabaseName);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //
        // Code for connecting Database
        //

        static ConnectDb()
        {
            AppDatabaseName = ConfigurationSettings.AppSettings["AppDatabaseName"];
        }
    }
}
