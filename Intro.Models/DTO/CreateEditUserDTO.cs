using System;
using System.Collections.Generic;
using System.Text;

namespace Intro.Models.DTO
{
    public class CreateEditUserDTO
    { /// <summary>
      /// Gets or sets the identifier.
      /// </summary>
      /// <value>
      /// The identifier.
      /// </value>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>
        /// The birth date.
        /// </value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the user type code.
        /// </summary>
        /// <value>
        /// The user type code.
        /// </value>
        public int UserTypeId { get; set; }

        /// <summary>
        /// Gets or sets the user title description.
        /// </summary>
        /// <value>
        /// The user title description.
        /// </value>
        public int UserTitleId { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }
    }
}