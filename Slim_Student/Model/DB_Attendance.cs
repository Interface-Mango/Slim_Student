using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Slim_Student.Model
{
    class DB_Attendance
    {
        DBManager db;

        public enum FIELD
        {
            id, sub_id, std_id, date, att_check, END
        }

        public DB_Attendance(DBManager _dbm)
        {
            db = _dbm;
        }

        public List<object[]> SelectAttendanceList(int sub_id, string std_id, string date)
        {
            string sql = "SELECT * FROM attendance WHERE sub_id=@arg1 AND std_id=@arg2 AND date LIKE '"+date+"%'";
            List<object> args = new List<object>();
            args.Add(sub_id);
            args.Add(std_id);

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

        public bool InsertStudentAttendance(string std_id, int sub_id, int check)
        {
            string sql = "INSERT INTO attendance(sub_id, std_id, att_check, date) VALUES(@arg1, @arg2, @arg3, NOW())";
            List<object> args = new List<object>();
            args.Add(sub_id);
            args.Add(std_id);
            args.Add(check);

            try
            {
                db.Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        cmd.Parameters.AddWithValue("@arg" + i, args.ElementAt(i - 1));
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

        public bool UpdateAttendance(int sub_id, string std_id, string date, int check)
        {
            string sql = "UPDATE attendance SET date=@arg1, att_check=@arg2 WHERE std_id=@arg3 AND sub_id=@arg4 AND date LIKE '"+date+"%'";
            List<object> args = new List<object>();
            args.Add(DateTime.Now);
            args.Add(check);
            args.Add(std_id);
            args.Add(sub_id);

            try
            {
                db.Connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, db.Connection))
                {
                    for (int i = 1; i <= args.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@arg" + i, args.ElementAt(i - 1));
                    }
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

        // 나중에 insert delete update 추가
    }
}
