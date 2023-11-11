namespace FluentPOS.Shared.DTOs.Sales.Enums
{
    public enum OrderStatus : byte
    {
        PendingApproval = 0,
        Pending = 1,
        Cancelled = 2,
        CityCorrection = 3,
        OnHold = 4,
        Approved = 5,
        AssignToOutlet = 6,
        ReQueueAfterReject = 7,
        AssignToHeadOffice = 8,
        Verifying = 9,
        Preparing = 10,
        ReadyToShip = 11,
        Shipped = 12,
        InProgress = 13,

    }
}