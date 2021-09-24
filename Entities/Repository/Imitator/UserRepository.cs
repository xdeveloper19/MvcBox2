using Entities.Context;
using Entities.Interfaces;
using Entities.Models.Imitator;
using Entities.Repository.Base;
using Entities.ViewModels.ImitatorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository.Imitator
{
    public class UserRepository : Repository<Owner>, IUserRepository
    {
        public UserRepository(ImitatorContext dbContext) : base(dbContext)
        {
        }

        public async Task<Owner> GetOnlyUserByIdAsync(Guid userId)
        {
            var category = (await GetAsync(m => m.Id == userId)).FirstOrDefault();
            return category;
        }

        //Реализация входа в приложение
        public async Task<Owner> GetOnlyUserByUsernameAsync(string email)
        {
            var category = (await GetAsync(m => m.Email.ToLower() == email.ToLower())).FirstOrDefault();
            return category;
        }

        public async Task<Guid> GetUserIdByUserNameAsync(string email)
        {
            var user = (await GetAsync(m => m.Email.ToLower() == email.ToLower())).FirstOrDefault();
            if (user != null)
                return user.Id;
            else return Guid.Empty;
        }

        //public async Task<IEnumerable<User>> GetUserByUserFIO(string userFIO)
        //{
        //    return await _dbContext.Users
        //         .Where(x => x.UserFIO.ToLower().StartsWith(userFIO.ToLower())).Take(15)
        //         .ToListAsync();
        //}

        //Регистрация пользователя
        public async Task<Owner> RegisterUser(UserModel model, byte[] passwordHash, byte[] passwordSalt)
        {
            var GetData = GetAsyncIQueryable(m => m.Email.ToLower() == model.Email.ToLower());
            if (GetData.Count() != 0)
            {
                throw new Exception("Пользователь с данным Email уже существует.");
            }
            var mappedEntity = Owner.Create(model.UserFIO, model.Email, passwordHash, passwordSalt);

            var newEntity = await AddAsync(mappedEntity);
            return newEntity;
        }
    }
}
