namespace FluentPOS.Shared.DTOs.Sales.Enums
{
    public enum OrderStatus : byte
    {
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
        AssignedToCSR = 14,
        IVRConfirmation = 15,
        IVRFailed = 16,
        WAConfirmation = 17,
        WAFailed = 18,
        Confirmed = 19,
        Returned = 20,
        Delivered = 21,
        Paid = 21,
        Closed = 22,
    }
}