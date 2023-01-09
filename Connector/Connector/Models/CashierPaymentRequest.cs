namespace Connector.Models;

public class CashierPaymentRequest
{
    public string ClientUniqueId { get; set; }

    public string MerchantId { get; set; }

    public string SiteId { get; set; }

    public string CustomerFirstname { get; set; }

    public string CustomerLastNane { get; set; }
}