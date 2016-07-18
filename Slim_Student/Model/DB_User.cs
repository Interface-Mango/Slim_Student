using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Slim_Student
{
    class DB_User
    {
        DBManager db;

        public enum FIELD{
            user_id, user_name, pw, auth, END
        }

        public DB_User(DBManager _dbm)
        {
            db = _dbm;
        }

        public object[] SelectUser(string id, string pw)
        {
            string sql = "SELECT * FROM user WHERE user_id=@arg1 AND pw=@arg2";
            List<object> args = new List<object>();
            args.Add(id);
            args.Add(pw);

            List<object[]> result = SearchDatas(sql, args);
            if (result.Count == 0)
                return null;
            else 
                return result[0];
        }

        public List<object[]> SearchDatas(string sql, List<object> args)
        {
            List<object[]> recordList = new List<object[]>();

            try
            {
                db.Connection.Open();
                MySqlDataReader reader;
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                    for (int i = 0; i < args.Count;i++ )
                        cmd.Parameters.AddWithValue("@arg"+(i+1), args[i]);
                    
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();

                    cmd.Connection = db.Connection;
                }

                while (reader.Read())
                {
                    object[] items = new object[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        items[i] = reader[i];
                    }
                    recordList.Add(items);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.Connection.Close();
            }

            return recordList;
        }
        
    }
}
