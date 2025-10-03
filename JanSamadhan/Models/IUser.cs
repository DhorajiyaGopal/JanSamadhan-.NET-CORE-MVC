using System.Collections.Generic;

namespace JanSamadhan.Models
{
    public interface IUser
    {
        User GetById(int id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(int id);

    }
}
