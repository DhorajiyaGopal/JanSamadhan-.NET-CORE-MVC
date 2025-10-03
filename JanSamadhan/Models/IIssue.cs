using System.Collections.Generic;

namespace JanSamadhan.Models
{
    public interface IIssue
    {
        Issue GetById(int id);
        IEnumerable<Issue> GetAll();
        IEnumerable<Issue> GetByUserId(int userId);
        void Add(Issue issue);
        void Update(Issue issue);
        void Delete(int id);
    }
}
