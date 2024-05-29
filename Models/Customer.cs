using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;
public class Customer
{
    [Key, ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<Vehicle> Vehicles { get; set; }
    public IEnumerable<ServiceOrder> ServiceOrders { get; set; }
}
