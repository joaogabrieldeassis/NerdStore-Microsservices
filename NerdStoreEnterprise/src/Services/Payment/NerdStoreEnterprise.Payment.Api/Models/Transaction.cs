using NerdStoreEnterprise.Core.DomainObjects;

namespace NerdStoreEnterprise.Payment.Api.Models;

public class Transaction : Entity
{
    public string AuthorizationCode { get; set; }
    public string CardBrand { get; set; }
    public DateTime? TransactionDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TransactionCost { get; set; }
    public TransactionStatus Status { get; set; }
    public string TID { get; set; } // Id
    public string NSU { get; set; } // Means (e.g., PayPal)

    public Guid PaymentId { get; set; }

    // EF Relation
    public Payment Payment { get; set; }
}
