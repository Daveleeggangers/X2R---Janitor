namespace X2R.Insight.Janitor.WebApi.Models
{
    public class QueryResult
    {
        public int ResultId { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public _Querys Query { get; set; }
        public QueryResult QueryResults { get; set; }
    }
}
