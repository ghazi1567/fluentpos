import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as moment from "moment";
import { environment } from "src/environments/environment";
import { CustomMomentDateAdapter } from "../helpers/custom-moment-date-adapter";

@Injectable({
    providedIn: "root"
})
export class BaseApiService {

    getDate(date): moment.Moment {
        var momentDate = moment(date);
        return momentDate.tz(CustomMomentDateAdapter.TIMEZONE);
    }
}
