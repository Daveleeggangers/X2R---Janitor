namespace X2R.Insight.Janitor.WebApi.Models
{
    public class Querys
    {
        public int TaskId { get; set; }
        public string Query { get; set; }
        public DateTime DateTime_Start { get; set; }
        public string RecurringSchedule { get; set; }
        public ICollection<QueryResult> QueryResult { get; set; }
    }
}
