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
            id, std_id, sub_id, content, date, END
        }

        public DB_MyQuestion(DBManager _dbm)
        {
            db = _dbm;
        }

        public List<object[]> SelectMyQuestionList(string std_id, int sub_id)
        {
            string sql = "SELECT * FROM my_question WHERE std_id=@arg1 AND sub_id=@arg2 ORDER BY id DESC";
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

        public bool InsertQuestion(string std_id, int sub_id, string content)
        {
            string sql = "INSERT INTO my_question(std_id, sub_id, content, date) VALUES(@arg1, @arg2, @arg3, NOW())";
            List<object> args = new List<object>();
            args.Add(std_id);
            args.Add(sub_id);
            args.Add(content);

            try
            {
                db.Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                    for (int i = 1; i <= 3;i++ )
                    {
                        cmd.Parameters.AddWithValue("@arg" + i, args.ElementAt(i-1));
                    }
                    cmd.ExecuteNonQuery();
                }
                db.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // For Debugging
                return false;    // 삽입 오류시 false 반환
            }

            return true;
        }

        public bool UpdateQuestion(int id, string content)
        {
            string sql = "UPDATE my_question SET content=@arg1 WHERE id=@arg2";
            List<object> args = new List<object>();
            args.Add(content);
            args.Add(id);

            try
            {
                db.Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                    cmd.Parameters.AddWithValue("@arg1", content);
                    cmd.Parameters.AddWithValue("@arg2", id);
                    cmd.ExecuteNonQuery();
                }
                db.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // For Debugging
                return false;    // 제거 오류시 false 반환
            }

            return true;
        }

        public bool DeleteQuestion(int id)
        {
            string sql = "DELETE FROM my_question WHERE id=@arg";

            try
            {
                db.Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                        cmd.Parameters.AddWithValue("@arg", id);
                    cmd.ExecuteNonQuery();
                }
                db.Connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  // For Debugging
                return false;    // 제거 오류시 false 반환
            }

            return true;
        }
    }
}
