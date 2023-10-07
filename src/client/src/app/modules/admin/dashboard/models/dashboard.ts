import { AttendanceStatus } from "src/app/core/enums/AttendanceStatus";
import { AttendanceType } from "src/app/core/enums/AttendanceType";
import { RequestStatus } from "src/app/core/enums/RequestStatus";

export interface Dashboard {
    totalEmployees: number;
    activeEmployees: number;
    presents: number;
    malePresents: number;
    femalePresents: number;

    absents: number;

    lateComer: number;
    last7DaysLateComer: number;
    last7DaysAbsents: number;
}
