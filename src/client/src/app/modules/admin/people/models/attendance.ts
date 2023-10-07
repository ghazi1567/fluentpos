import { AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
import { AttendanceType } from "src/app/core/enums/AttendanceType";
import { RequestStatus } from "src/app/core/enums/RequestStatus";

export interface Attendance {
    id: string;

    createaAt: string;

    updatedAt: string;

    organizationId: string;

    branchId: string;

    employeeId: string;

    departmentId: string;

    policyId: string;

    designationId: string;

    attendanceDate: Date;

    addedOn: Date;

    attendanceType: AttendanceType;

    attendanceStatus: AttendanceStatus;

    requestId: string;

    actualIn: Date;

    actualOut: Date;

    expectedIn: Date;

    expectedOut: Date;

    checkIn: Date;

    checkOut: Date;
    checkInTime: Date;

    checkOutTime: Date;
    status: RequestStatus;

    statusUpdateOn: Date;

    approvedBy: string;

    reason: string;

    earnedHours: number;

    earnedMinutes: number;

    overtimeHours: number;

    overtimeMinutes: number;

    bioMachineId: string;

    deductedHours: number;

    lateMinutes: number;

    totalEarnedHours: number;

    actualEarnedHours: number;
    employeeName: string;
    statusName: string;
    attendanceStatusName: string;
    attendanceTypeName: string;
    View: boolean;
    Update: boolean;
    Remove: boolean;
    className?: string;
    isCheckOutMissing: boolean;
    isLateComer: boolean;
    punchCode?: number;
    departmentName?: string;
    isNextDay: boolean;
}
