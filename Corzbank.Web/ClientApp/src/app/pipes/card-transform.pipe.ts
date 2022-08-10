import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
	name: "card",
})
export class CardTranformPipe implements PipeTransform {
	transform(creditCard: string): string {
		return creditCard
			.replace(/\s+/g, "")
			.replace(/(\d{4})/g, "$1 ")
			.trim();
	}
}
