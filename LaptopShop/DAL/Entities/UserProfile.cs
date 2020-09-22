using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.DAL.Entities
{
    /// <summary>
    /// Base info user
    /// </summary>
    [Table("tblUserProfiles")]
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public long Id { get; set; }

        [Required, StringLength(75)]
        public string FirstName { get; set; }
        
        [Required, StringLength(75)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string Photo { get; set; }

        /// <summary>
        /// Фото користувача
        /// </summary>
        [StringLength(150)]
        public string Image { get; set; }

        /// <summary>
        /// Дата останнього входження
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Дата реєстрації
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        public virtual DbUser User { get; set; }
    }
}
