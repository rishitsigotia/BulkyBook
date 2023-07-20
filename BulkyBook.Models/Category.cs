using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]  // it will now show like this "Display Order" previously it was DisplayOrder
        [Range(1, 100, ErrorMessage = "Enter the quantity b/w 1 to 100 !!")] // Range Validation Attribute
        public int DisplayOrder { get; set; }


        public DateTime OrderCreated { get; set; } = DateTime.Now;
    }
}
