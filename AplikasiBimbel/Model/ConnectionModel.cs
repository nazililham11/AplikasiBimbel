namespace AplikasiBimbel.Model
{
    public class ConnectionModel
    {
        public string DatabaseHost { get; set; }
        public string DatabasePort { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseName { get; set; }

        public string GetMySQLConnectionString()
        {
            return $"datasource={DatabaseHost};port={DatabasePort}" +
                   $";username={DatabaseUsername};password={DatabasePassword}" +
                   $";database={DatabaseName};";
        }

        public string GetMSSQLConnectionString()
        {
            //return @"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog = DatabaseBimbel; " +
            //       @"User ID = sa; Password = ;";
            return $"Data Source = {DatabaseHost}; Initial Catalog = {DatabaseName}; " +
                   $"User ID = {DatabaseUsername}; Password = {DatabasePassword};";
        }
    }
}
