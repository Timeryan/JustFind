import { Pipe, PipeTransform } from "@angular/core";
import { Statuses } from "src/app/models/statuses";

@Pipe({
    name: 'getStatus',
    pure: true
  })
  export class GetStatusPipe implements PipeTransform {
  
    transform(status: string, args?: any): string {
      return this.getStatus(status);
    }
    public getStatus(status) : string {
        switch(status){
          case Statuses.Closed: return "Снято с публикации";
          case Statuses.ModerationRequired: return "Требуется проверка";
          case Statuses.NotAvailable: return "Недоступно";
          case Statuses.OnSale: return "Опубликовано";
          case Statuses.Rejected: return "Отклонено";
          case Statuses.Remoderation: return "Повторная проверка";
        }
    
        return "";
      }
    }