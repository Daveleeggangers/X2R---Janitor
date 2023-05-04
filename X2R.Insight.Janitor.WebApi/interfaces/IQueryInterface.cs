using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.interfaces
{
    public interface IQueryInterface
    {
        ICollection<Querys> GetQuerys();
        ICollection<Querys> GetDateTimesQuery();
        Querys GetQuery(int id);
        bool QueryExists(int id);
        bool CreateQuery(Querys query);
        bool ChangeDetails(int id, string details);
        bool ChangeStatus(int id);
        string GetStatus(int id);
        int ExecuteQuery(string query);
    }
}
