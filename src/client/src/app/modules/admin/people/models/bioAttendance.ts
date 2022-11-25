import { AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
import { AttendanceType } from "src/app/core/enums/AttendanceType";
import { RequestStatus } from "src/app/core/enums/RequestStatus";

export interface BioAttendance {
    id: string;

    createaAt: string;

    updatedAt: string;

    organizationId: string;

    branchId: string;

    punchCode: string;

    name: string;

    cardNo: string;

    attendanceDateTime: Date;

    attendanceDate: Date;

    attendanceTime: Date;

    direction: string;

    deviceSerialNo: string;

    deviceName: string;

    temperature: string;

    abnormal: string;
}
