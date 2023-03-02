import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'customDateFormet'
})
export class CustomDateFormetPipe implements PipeTransform {

  transform(value: Date | moment.Moment, dateFormat: string): any {
    return moment(value).utcOffset('+0500').format(dateFormat);
  }

}
