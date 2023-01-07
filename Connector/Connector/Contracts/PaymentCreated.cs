namespace Connector.Contracts;

public class PaymentCreated
{
    public string MerchantId { get; set; }
    public string SiteId { get; set; }
    public string ProviderTransactionId { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}