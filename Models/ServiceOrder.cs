using SmartGarage.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;
public class ServiceOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }

    public DateTime Date { get; set; }

    public ServiceOrderStatus Status { get; set; }
    public bool IsDeleted { get; set; }

    // Additional properties (e.g., status) based on project requirements
}
