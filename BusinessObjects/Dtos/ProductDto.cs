using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Dtos
{
    public class ProductDto
    {

        [Required]
        [StringLength(40)]
        public string? ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
