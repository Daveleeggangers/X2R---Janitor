using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.interfaces
{
    public interface IQueryInterface
    {
        ICollection<Querys> GetQuerys();

        Querys GetQuery(int id);

        bool QueryExists(int id);

        bool CreateQuery(Querys query);

        string GetStatus(int id);

        ICollection<Querys> GetDateTimesQuery();
        int ExecuteQuery(string query);

        bool ChangeDetails(int id, string details);
    }
}
