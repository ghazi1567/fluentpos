export enum OrderStatus {
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
    Rejected = 14
}

export const OrderStatusMapping: Record<OrderStatus, string> = {
    [OrderStatus.PendingApproval]: "Pending Approval",
    [OrderStatus.Pending]: "Pending",
    [OrderStatus.AssignToOutlet]: "Assigned To Outlet",
    [OrderStatus.Verifying]: "Verifying",
    [OrderStatus.Preparing]: "Preparing",
    [OrderStatus.ReadyToShip]: "Ready To Ship",
    [OrderStatus.Shipped]: "Shipped",
    [OrderStatus.Cancelled]: "Cancelled",
    [OrderStatus.Approved]: "Approved",
    [OrderStatus.InProgress]: "In Progress",
    [OrderStatus.Rejected]: "Rejected",
    [OrderStatus.CityCorrection]: "City Correction",
    [OrderStatus.OnHold]: "On Hold",
    [OrderStatus.ReQueueAfterReject]: "Requeue",
    [OrderStatus.AssignToHeadOffice]: "Assigned To Head Office"
};
