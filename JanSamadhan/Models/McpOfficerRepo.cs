using JanSamadhan.Data;
using System.Collections.Generic;
using System.Linq;

namespace JanSamadhan.Models
{
    public class McpOfficerRepo : IMcpOfficer
    {
        private readonly AppDbContext _context;
        public McpOfficerRepo(AppDbContext context)
        {
            _context = context;
        }

        public McpOfficer GetById(int id)
        {
            return _context.McpOfficers.FirstOrDefault(x => x.Id == id);
        }

        public McpOfficer GetByEmail(string email)
        {
            return _context.McpOfficers.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<McpOfficer> GetAll()
        {
            return _context.McpOfficers.ToList();
        }

        public void Add(McpOfficer mcpOfficer)
        {
            _context.McpOfficers.Add(mcpOfficer);
            _context.SaveChanges();
        }

        public void Update(McpOfficer mcpOfficer)
        {
            _context.McpOfficers.Update(mcpOfficer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var mcpOfficer = GetById(id);
            if (mcpOfficer != null)
            {
                _context.McpOfficers.Remove(mcpOfficer);
                _context.SaveChanges();
            }
        }


    }
}
