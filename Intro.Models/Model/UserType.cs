using System;
using System.Collections.Generic;

#nullable disable

namespace Intro.Models.Model
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
