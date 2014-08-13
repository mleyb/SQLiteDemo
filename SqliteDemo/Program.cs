using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Transactions;

namespace SqliteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection.CreateFile("DPCAdapter.sqlite");

            using (var connection = new SQLiteConnection("Data Source=DPCAdapter.sqlite;Version=3;"))
            {
                connection.Open();

                using (TransactionScope tran = new TransactionScope())
                {
                    string sql1 = "CREATE TABLE Events (Created integer, Content nvarchar)";

                    SQLiteCommand command1 = new SQLiteCommand(sql1, connection);
                    command1.ExecuteNonQuery();

                    string sql2 = "CREATE INDEX CreatedIndex ON Events(Created)";

                    SQLiteCommand command2 = new SQLiteCommand(sql2, connection);
                    command2.ExecuteNonQuery();
                    //Insert create script here.

                    //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                    tran.Complete();
                }

                using (TransactionScope tran = new TransactionScope())
                {

                    for (int i = 0; i < 5000; i++)
                    {
                        DateTime now = DateTime.Now;

                        string sql2 = "INSERT INTO Events (Created, Content) VALUES ('" + now.ToUnixTime() + "', 'Some event content Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content  Some event content ')";

                        SQLiteCommand command2 = new SQLiteCommand(sql2, connection);
                        command2.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }

            Console.ReadLine();



        }
    }

    public static class DateTimeExtension
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}
