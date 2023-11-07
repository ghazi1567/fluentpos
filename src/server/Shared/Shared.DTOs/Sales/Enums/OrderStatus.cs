namespace FluentPOS.Shared.DTOs.Sales.Enums
{
    public enum OrderStatus : byte
    {
        PendingApproval = 0,
        Pending = 1,
        Cancelled = 2,
        Approved = 3,
        AssignToOutlet = 4,
        Verifying = 5,
        Preparing = 6,
        ReadyToShip = 7,
        Shipped = 8,
        InProgress = 9,
    }
}