using System.ComponentModel.DataAnnotations;

namespace Cashier.Controllers;

public class CashierPaymentCreate
{
    [Required]
    public string ClientUniqueId { get; set; }
    [Required]
    public string MerchantId { get; set; }
    [Required]
    public string SiteId { get; set; }
    [Required]
    [MaxLength(30)]
    public string CustomerFirstname { get; set; }
    [Required]
    [MaxLength(40)]
    public string CustomerLastNane { get; set; }
}