namespace Connector.Entities;

public class Payment
{
    public string Id { get; set; }
    public string MerchantPaymentId { get; set; }
    public string MerchantId { get; set; }
    public string SiteId { get; set; }
}