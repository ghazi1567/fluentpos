export enum RequestStatus {
    Pending = 0,
    Rejected = 1,
    Approved = 2,
    InProgress = 3
}
export const RequestStatusMapping: Record<RequestStatus, string> = {
    [RequestStatus.Approved]: "Approved",
    [RequestStatus.Rejected]: "Rejected",
    [RequestStatus.Pending]: "Pending",
    [RequestStatus.InProgress]: "In Progress",
};
