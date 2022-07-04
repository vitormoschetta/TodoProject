namespace Todo.Infrastructure.Database.Models.NoSql
{
    public class DatabaseNoSqlSettings   
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TodoCollectionName { get; set; }
    }
}