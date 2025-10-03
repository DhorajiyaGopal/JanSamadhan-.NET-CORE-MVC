using JanSamadhan.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JanSamadhan.Models
{
    public class ReplyRepo : IReply
    {
        private readonly AppDbContext _context;
        public ReplyRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Reply reply)
        {
            _context.Replies.Add(reply);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Replies.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Reply> GetAll()
        {
            return _context.Replies
                .Include(r => r.Issue)
                .Include(r => r.McpOfficer)
                .ToList();
        }

        public Reply GetById(int id)
        {
            return _context.Replies
                    .Include(r => r.Issue)
                    .Include(r => r.McpOfficer)
                    .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reply> GetByOfficerId(int userId)
        {
            return _context.Replies
                    .Include(r => r.Issue)
                    .Include(r => r.McpOfficer)
                    .Where(i => i.McpOfficerId == userId)
                    .ToList();
        }

        public void Update(Reply reply)
        {
            _context.Replies.Update(reply);
            _context.SaveChanges();
        }
    }
}