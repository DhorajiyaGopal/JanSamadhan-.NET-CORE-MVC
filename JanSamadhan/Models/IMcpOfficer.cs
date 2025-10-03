using System.Collections.Generic;

namespace JanSamadhan.Models
{
    public interface IMcpOfficer
    {
        McpOfficer GetById(int id);
        McpOfficer GetByEmail(string email);
        IEnumerable<McpOfficer> GetAll();
        void Add(McpOfficer mcpOfficer);
        void Update(McpOfficer mcpOfficer);
        void Delete(int id);

    }
}
