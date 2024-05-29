using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;
public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Range(0, double.MaxValue)] // Non-negative price
    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<ServiceOrder> ServiceOrders { get; set; }
}
