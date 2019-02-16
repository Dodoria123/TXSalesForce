using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXSalesForce.UWP;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace TXSalesForce.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "TXSalesForceDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
