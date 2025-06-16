using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Users.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorFullName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string LastUpdateUserId { get; set; }
        public string LastUpdateFullName { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteUserId { get; set; }
        public string DeleteFullName { get; set; }
        public User()
        {
            IsActive = true;
            CreateTime = DateTime.Now;
            ConcurrencyStamp = Guid.NewGuid().ToString();
            LastUpdate = null;
            IsDelete = false;
            DeleteTime = null;
        }
    }
}
