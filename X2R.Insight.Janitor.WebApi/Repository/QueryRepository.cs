using Microsoft.EntityFrameworkCore;
using X2R.Insight.Janitor.WebApi.Data;
using X2R.Insight.Janitor.WebApi.interfaces;
using X2R.Insight.Janitor.WebApi.Models;

namespace X2R.Insight.Janitor.WebApi.Repository
{
    public class QueryRepository : IQueryInterface
    {
        private readonly QueryContext _context;

        public QueryRepository(QueryContext context)
        {
            _context = context;
        }

        public Querys GetQuery(int id)
        {
            return _context.Querys.Where(p => p.TaskId == id).FirstOrDefault();
        }

        public ICollection<Querys> GetQuerys()
        {
            return _context.Querys.OrderBy(p => p.TaskId).ToList();
        }

        public ICollection<Querys> GetDateTimesQuery()
        {
            return _context.Querys.OrderBy(p => p.TaskId).ToList();
        }

        public bool QueryExists(int id)
        {
            return _context.Querys.Any(p => p.TaskId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public string GetStatus(int id)
        {
            return _context.QueryResult.Where(p => p.ResultId == id).Select(p => p.Status).SingleOrDefault();
        }

        public bool CreateQuery(Querys query)
        {
            _context.Add(query);
            return Save();
        }

        public bool ChangeDetails(int id, string details)
        {
            details = $"{details} row(s) affected";

            _context.Database
                .ExecuteSql($"UPDATE QueryResult SET Details = {details} WHERE ResultId = {id};");
            return true;
        }
        public bool ChangeStatus(int id)
        {
            _context.Database
                .ExecuteSql($"UPDATE QueryResult SET Status = 'Inactive' WHERE ResultId = {id};");
            return true;
        }

        public int ExecuteQuery(string query)
        {
            var blogs = _context.Database
                .ExecuteSql($"UPDATE Querys SET query = 'Alfred Schmidt' WHERE TaskId = 2;");
            return blogs;
        }
    }
}
