import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'localePrice'
})
export class LocalePricePipe implements PipeTransform {

  transform(value: number, ...args: unknown[]): string {
    return value.toString() + ' ₽';
  }

}
