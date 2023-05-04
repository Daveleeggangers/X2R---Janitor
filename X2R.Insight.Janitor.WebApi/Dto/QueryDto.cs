namespace X2R.Insight.Janitor.WebApi.Dto
{
    public class QueryDto
    {
        public int TaskId { get; set; }
        public string Query { get; set; }
        public string DateTime_Start { get; set; }
        public string RecurringSchedule { get; set; }
    }
}
