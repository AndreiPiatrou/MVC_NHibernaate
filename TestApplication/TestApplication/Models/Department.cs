using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TestApplication.Models
{
    public class Department : IEntity<int>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }
    }
}