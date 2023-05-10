using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.interfaces
{
    public interface IQueryInterface
    {
        ICollection<_Querys> GetQuerys();
        ICollection<_Querys> GetDateTimesQuery();
        _Querys GetQuery(int id);
        bool QueryExists(int id);
        bool CreateQuery(_Querys query);
        bool ChangeDetails(int id, string details);
        bool ChangeStatus(int id);
        string GetStatus(int id);
        int ExecuteQuery(string query);
    }
}
