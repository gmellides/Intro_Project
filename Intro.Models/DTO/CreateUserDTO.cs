using System;
using System.Collections.Generic;
using System.Text;

namespace Intro.Models.DTO
{
    public class CreateUserDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserTypeDescription { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTitleDescription { get; set; }
        public string EmailAddress { get; set; }
    }
}
