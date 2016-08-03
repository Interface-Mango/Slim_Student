using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Slim_Student.Model
{
    class DB_MyQuestion
    {
        DBManager db;

        public enum FIELD{
            id, std_id, sub_id, content, END
        }

        public DB_MyQuestion(DBManager _dbm)
        {
            db = _dbm;
        }

        public List<object[]> SelectMyQuestionList(int std_id, int sub_id)
        {
            string sql = "SELECT * FROM my_question WHERE std_id=@arg1 AND sub_id=@arg2";
            List<object> args = new List<object>();
            args.Add(std_id);
            args.Add(sub_id);

            List<object[]> result = SearchDatas(sql, args);
            if (result.Count == 0)
                return null;
            else
                return result;
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
                    for (int i = 0; i < args.Count; i++)
                        cmd.Parameters.AddWithValue("@arg" + (i + 1), args[i]);

                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();

                    cmd.Connection = db.Connection;
                }

                while (reader.Read())
                {
                    object[] items = new object[reader.FieldCount]; // columns 개수
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
