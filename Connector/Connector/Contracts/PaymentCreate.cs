namespace Connector.Contracts;

public class PaymentCreate
{
    public string MerchantTransactionId { get; set; }
    public string MerchantId { get; set; }
    public string SiteId { get; set; }
    public string Amount { get; set; }
    public string Currency { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
}