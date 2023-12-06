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

    pending: number;

    reQueued: number;

    assignedToOutlet: number;

    assignedToHeadOffice: number;

    approved: number;

    shipped: number;

    preparing: number;

    readyToShip: number;

    verifying: number;

    cityCorrection: number;

    cancelled: number;

}
