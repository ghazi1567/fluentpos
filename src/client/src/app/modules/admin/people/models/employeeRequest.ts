import { RequestStatus } from "src/app/core/enums/RequestStatus";
import { RequestType } from "src/app/core/enums/RequestType";

export interface EmployeeRequest {
    id: string;

    createaAt: string;

    updatedAt: string;

    organizationId: string;

    branchId: string;

    employeeId: string;

    departmentId: string;

    policyId: string;

    designationId: string;

    requestType: RequestType;

    requestedOn: Date;

    requestedBy: string;

    attendanceDate: Date;

    checkIn: Date;

    checkOut: Date;

    overtimeHours: number;

    overTimeType: string;

    reason: string;

    status: RequestStatus;

    statusUpdateOn: string;

    workflowId: string;

    assignedTo: string;

    assignedOn: Date;

    statusName: string;
    View: boolean;
    Update: boolean;
    Remove: boolean;
    requestedByName: string;
    requestedForName: string;

}
