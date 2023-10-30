export enum OrderStatus {
    None = 0,
    Pending = 1,
    AssignToOutlet = 2,
    Verifying = 3,
    Preparing = 4,
    ReadyToShip = 5,
    Shipped = 6,
    Cancelled = 7,
    Approved = 8,
    InProgress = 9,
    PendingApproval = 10,
    Rejected = 11
}

export const OrderStatusMapping: Record<OrderStatus, string> = {
    [OrderStatus.None]: "-",
    [OrderStatus.Pending]: "Pending",
    [OrderStatus.AssignToOutlet]: "AssignToOutlet",
    [OrderStatus.Verifying]: "Verifying",
    [OrderStatus.Preparing]: "Preparing",
    [OrderStatus.ReadyToShip]: "ReadyToShip",
    [OrderStatus.Shipped]: "Shipped",
    [OrderStatus.Cancelled]: "Cancelled",
    [OrderStatus.Approved]: "Approved",
    [OrderStatus.InProgress]: "InProgress",
    [OrderStatus.PendingApproval]: "PendingApproval",
    [OrderStatus.Rejected]: "Rejected",
};
