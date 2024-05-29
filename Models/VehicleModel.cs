using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models;
public class VehicleModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(25)]
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}