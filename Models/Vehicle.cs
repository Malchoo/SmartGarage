using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;
public class Vehicle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [RegularExpression(@"^[A-Z]{2}\d{4}[A-Z]{2}$")] // Valid Bulgarian license plate
    public string LicensePlate { get; set; }

    [Required]
    [StringLength(17)]
    public string VIN { get; set; }

    [Required]
    [Range(1887, int.MaxValue)] // Year of creation > 1886
    public int YearOfCreation { get; set; }

    public int ModelId { get; set; }
    public VehicleModel Model { get; set; }
    public int BrandId { get; set; }
    public VehicleBrand Brand { get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<ServiceOrder> ServiceOrders { get; set; }
    public bool IsDeleted { get; set; }

}
