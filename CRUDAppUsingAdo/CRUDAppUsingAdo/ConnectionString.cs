namespace CRUDAppUsingAdo
{
    public static class ConnectionString
    {
        private static string connString = "Server=localhost\\SQLEXPRESS;Database=CrudADOdb;Trusted_Connection=True;";

        public static string dbcs
        {
            get
            {
                return connString;
            }
        }
    }
}
