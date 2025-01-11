namespace NerdStoreEnterprise.Order.Domain.Orders;

public enum OrderStatus
{
    Authorized = 1,
    Paid = 2,
    Rejected = 3,
    Delivered = 4,
    Canceled = 5
}