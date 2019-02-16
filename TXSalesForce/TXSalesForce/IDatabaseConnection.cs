using System;
using System.Collections.Generic;
using System.Text;

namespace TXSalesForce
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
