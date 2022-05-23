import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name:'phone'
})

export class PhoneTranformPipe implements PipeTransform{
    transform(phoneNumber: string): string {
        return phoneNumber.replace(/(\d{1})(\d{2})(\d{2})(\d{3})/, '0$1-$2-$3-$4');
      }
    }