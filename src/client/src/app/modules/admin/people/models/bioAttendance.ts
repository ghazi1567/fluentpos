import { AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
import { AttendanceType } from "src/app/core/enums/AttendanceType";
import { RequestStatus } from "src/app/core/enums/RequestStatus";

export interface BioAttendance {
    id: string;

    createaAt: string;

    updatedAt: string;

    organizationId: string;

    branchId: string;

    personId: string;

    name: string;

    department: string;

    attendanceDateTime: Date;

    attendanceStatus: string;

    customName: string;

    attendanceCheckPoint: string;

    dataSource: string;

    handlingType: string;

    temperature: string;

    abnormal: string;
}
