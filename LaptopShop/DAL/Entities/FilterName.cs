using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.DAL.Entities
{
    [Table("tblFilterNames")]
    public class FilterName
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<Filter> Filtres { get; set; }
        public virtual ICollection<FilterNameGroup> FilterNameGroups { get; set; }
    }
}
