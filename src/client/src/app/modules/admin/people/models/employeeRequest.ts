import { AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
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
    
    checkInTime: Date;

    checkOutTime: Date;

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
    modificationId?: string;
    attendanceStatus: AttendanceStatus;
    attendanceStatusName: string;
    requestTypeName: string;
    isNextDay: boolean;
}
