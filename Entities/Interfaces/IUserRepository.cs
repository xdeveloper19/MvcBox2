using Entities.Models.Imitator;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IUserRepository : IRepository<Owner>
    {
        //Task<User> GetUserWithDailyRowsAsync(Guid userId);
        Task<Owner> GetOnlyUserByIdAsync(Guid userId);
        Task<Owner> RegisterUser(UserModel model, byte[] paswordHash, byte[] passwordSalt);
        Task<Guid> GetUserIdByUserNameAsync(string userName);
        //Task<IEnumerable<User>> GetUserByUserFIO(string userFIO);
        Task<Owner> GetOnlyUserByUsernameAsync(string userName);
    }
}
