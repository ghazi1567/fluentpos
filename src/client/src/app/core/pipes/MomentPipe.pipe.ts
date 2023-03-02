import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment-timezone';

@Pipe({ name: 'dateFormat' })
export class MomentPipe implements PipeTransform {
    transform(value: Date | moment.Moment, dateFormat: string): any {
        return moment(value).format(dateFormat);
    }
}