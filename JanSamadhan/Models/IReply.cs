using System.Collections.Generic;

namespace JanSamadhan.Models
{
    public interface IReply
    {
        Reply GetById(int id);
        IEnumerable<Reply> GetAll();
        IEnumerable<Reply> GetByOfficerId(int userId);
        void Add(Reply reply);
        void Update(Reply reply);
        void Delete(int id);
    }
}
