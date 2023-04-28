namespace X2R.Insight.Janitor.DataAccess
{
    public class Variables
    {
        private static string DbName = Environment.GetEnvironmentVariable("DBNAME") ?? "X2R_Janitor";
        private static string Server = Environment.GetEnvironmentVariable("DBSERVER") ?? "127.0.0.1";
        private static string Username = Environment.GetEnvironmentVariable("DBUSER") ?? "root";
        private static string Password = Environment.GetEnvironmentVariable("DBPASS") ?? "";

        public static string ConnectionString = $"server={Server};port=3306;database={DbName};uid={Username};password={Password}";

    }
}
