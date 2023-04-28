using MySql.Data.MySqlClient;
using System.Collections.Generic;
using X2R.Insight.Janitor.DataAccess;

namespace X2R.Insight.Janitor.Logic
{
    public class Logic
    {
        public bool databaseConnection()
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool UseExecuteTimes(out string query)
        {
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);
            try
            {
                string oString = "SELECT * FROM querys LEFT JOIN queryresult ON queryresult.ResultId = querys.TaskId;";
                MySqlCommand oCmd = new MySqlCommand(oString, connection);
                connection.Open();
                using (MySqlDataReader oReader = oCmd.ExecuteReader())
                {
                    var amount = string.Empty;
                    List<string> querys = new List<string>() { };
                    Console.WriteLine("Query's that hasnt happen yet:");
                    while (oReader.Read())
                    {
                        if (Convert.ToString(oReader["Status"]) == "Active")
                        {
                            try
                            {
                                var dateNow = DateTime.Now;
                                var queryDate = Convert.ToDateTime(oReader["DateTime Start"]);

                                if (dateNow < queryDate)
                                {
                                    Console.WriteLine("Query: " + oReader["Query"] + " -------- Expected execute time: " + oReader["DateTime Start"]);
                                }
                                else if (dateNow == queryDate || dateNow > queryDate)
                                {
                                    Console.WriteLine("Query: " + oReader["Query"] + " -------- Expected execute time: " + oReader["DateTime Start"]);
                                    var complete = oReader["TaskId"] + ";" + oReader["Query"].ToString();
                                    querys.Add(complete);
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Something went wrong");
                                Console.WriteLine(ex);
                            }
                        }
                    }
                    ExecuteQuery(querys, Convert.ToInt16(oReader["TaskId"]), oReader["Details"].ToString());
                    query = oReader["Query"].ToString();
                }
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                query = string.Empty;
                return false;
            }

        }

        protected bool ExecuteQuery(List<string> querys, int id, string details)
        {
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);

            Console.WriteLine("The following querys will be executed:");
            bool chk = !querys.Any();
            if (chk == true)
            {
                Console.WriteLine("No querys found to execute");
                return true;
            }
            else
            {
                try
                {
                    connection.Open();
                    foreach (var query in querys)
                    {
                        var Id = query.Split(";");
                        var sQuery = query.Split(";");

                        Console.WriteLine(sQuery[1]);
                        string oString = sQuery[1];
                        string amount = string.Empty;
                        MySqlCommand oCmd = new MySqlCommand(oString, connection);
                        ScheduleById(Convert.ToInt32(Id[0]), out amount);
                        long count = 0;
                        int rowsChanged = 0;
                        rowsChanged = oCmd.ExecuteNonQuery();
                        if (rowsChanged < 0)
                        {
                            count = (int)(long)oCmd.ExecuteScalar();
                            Console.WriteLine("-Amount of records: " + count);
                            ChangeDetails($"{count} Record(s) shown", Convert.ToInt32(Id[0]));
                        }
                        else
                        {
                            Console.WriteLine("-Rows changed: " + rowsChanged);
                            ChangeDetails($"{rowsChanged} Row(s) changed", Convert.ToInt32(Id[0]));
                        }
                        if (count == 0 && rowsChanged == 0)
                        {
                            Console.WriteLine("no rows are effected");
                        }
                        if (amount == "Once")
                        {
                            ChangeAfterComplete(Convert.ToInt32(Id[0]));
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Query went wrong: " + ex);
                    return false;
                }
            }
        }

        public bool ChangeAfterComplete(int id)
        {
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);

            try
            {
                string oString = $"UPDATE `querys` LEFT JOIN queryresult ON queryresult.ResultId = querys.TaskId SET `Status` = 'Inactive' WHERE `querys`.`TaskId` = {id}";
                MySqlCommand oCmd = new MySqlCommand(oString, connection);
                connection.Open();
                using (MySqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        if (oReader.Read() == true)
                        {
                            Console.WriteLine("noice");
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            connection.Close();
            return false;
        }

        public bool ScheduleById(int id, out string amount)
        {
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);

            try
            {
                string oString = $"SELECT * FROM querys LEFT JOIN queryresult ON queryresult.ResultId = querys.TaskId WHERE TaskId = {id}";
                MySqlCommand oCmd = new MySqlCommand(oString, connection);
                connection.Open();
                using (MySqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        amount = oReader["RecurringSchedule"].ToString();
                        return true;
                    }
                }
            }
            catch
            {
                amount = string.Empty;
                return false;
            }
            connection.Close();
            amount = string.Empty;
            return true;
        }

        public bool ChangeDetails(string details, int id)
        {
            MySqlConnection connection = new MySqlConnection(Variables.ConnectionString);

            try
            {
                string oString = $"UPDATE `querys` LEFT JOIN queryresult ON queryresult.ResultId = querys.TaskId SET `Details` = '{details}' WHERE `querys`.`TaskId` = {id}";
                MySqlCommand oCmd = new MySqlCommand(oString, connection);
                connection.Open();
                using (MySqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        return true;
                    }
                }
            }
            catch (Exception eX)
            {
                Console.WriteLine(eX);
                return false;
            }
            connection.Close();
            return true;
        }
    }
}
