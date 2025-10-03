using JanSamadhan.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JanSamadhan.Models
{
    public class IssueRepo : IIssue
    {
        private readonly AppDbContext _context;

        public IssueRepo(AppDbContext context)
        {
            _context = context;
        }

        public Issue GetById(int id)
        {
            return _context.Issues
                .Include(i => i.User)
                .Include(i => i.Replies)
                    .ThenInclude(r => r.McpOfficer)
                .FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Issue> GetAll()
        {
            return _context.Issues
                .Include(i => i.User)
                .Include(i => i.Replies)
                    .ThenInclude(r => r.McpOfficer)
                .ToList();
        }

        public IEnumerable<Issue> GetByUserId(int userId)
        {
            return _context.Issues
                .Include(i => i.Replies)
                    .ThenInclude(r => r.McpOfficer)
                .Where(i => i.UserId == userId)
                .ToList();
        }

        public void Add(Issue issue)
        {
            _context.Issues.Add(issue);
            _context.SaveChanges();
        }

        public void Update(Issue issuechanges)
        {
            _context.Issues.Update(issuechanges);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var issue = GetById(id);
            if (issue != null)
            {
                _context.Issues.Remove(issue);
                _context.SaveChanges();
            }
        }
    }
}