using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public int ExecuteQuery(string query)
        {
            var blogs = _context.Database
                .ExecuteSql($"UPDATE Querys SET query = 'Alfred Schmidt' WHERE TaskId = 1;");
            return blogs;
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
            var status = _context.Database
                .ExecuteSql($"UPDATE QueryResult SET Status = 'Inactive' WHERE ResultId = {id};");

            var _details = _context.Database
                .ExecuteSql($"UPDATE QueryResult SET Details = '{details} Row(s) affected' WHERE ResultId = {id};");
            return true;
        }
    }
}
