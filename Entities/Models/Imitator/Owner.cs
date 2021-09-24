using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Imitator
{
    public class Owner: EntityBase
    {
        public string UserFIO { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Признак допуска в систему
        /// </summary>
        public bool IsAllowed { get; set; }

        public Device Device { get; set; }


        public static Owner Create(string userFIO, string Email, byte[] passwordHash, byte[] passwordSalt, bool IsAllowed = false, string description = null)
        {
            var user = new Owner
            {
                UserFIO = userFIO,
                Description = description,
                Email = Email,
                IsAllowed = IsAllowed,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return user;
        }
    }
}
