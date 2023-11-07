export enum OrderStatus {
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
    Rejected = 10
}

export const OrderStatusMapping: Record<OrderStatus, string> = {
    [OrderStatus.PendingApproval]: "PendingApproval",
    [OrderStatus.Pending]: "Pending",
    [OrderStatus.AssignToOutlet]: "AssignToOutlet",
    [OrderStatus.Verifying]: "Verifying",
    [OrderStatus.Preparing]: "Preparing",
    [OrderStatus.ReadyToShip]: "ReadyToShip",
    [OrderStatus.Shipped]: "Shipped",
    [OrderStatus.Cancelled]: "Cancelled",
    [OrderStatus.Approved]: "Approved",
    [OrderStatus.InProgress]: "InProgress",
    [OrderStatus.Rejected]: "Rejected",
};
