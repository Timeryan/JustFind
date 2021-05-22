import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'timePassed',
})
export class TimePassedPipe implements PipeTransform {
  transform(value: any, ...args: unknown[]): string {
    return moment.utc(value).fromNow();
  }
}
