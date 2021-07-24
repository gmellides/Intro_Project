using System;
using System.Collections.Generic;

#nullable disable

namespace Intro.Models.Model
{
    public partial class UserTitle
    {
        public UserTitle()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
