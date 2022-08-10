import { Component, OnInit } from "@angular/core";
import { Guid } from "guid-typescript";
import { CardDTO } from "src/app/data/dtos/card.dto";
import { TransferDTO } from "src/app/data/dtos/transfer.dto";
import { CardType } from "src/app/data/enums/card-type.enum";
import { PaymentSystem } from "src/app/data/enums/payment-system.enum";
import { StorageTypeEnum } from "src/app/data/enums/storage-type.enum";
import { TransferType } from "src/app/data/enums/transfer-type.enum";
import { Constants } from "src/app/data/helpers/constants";
import { CardService } from "src/app/data/services/card.service";
import { StorageService } from "src/app/data/services/storage.service";
import { TransferService } from "src/app/data/services/transfer.service";

@Component({
	selector: "app-wallet",
	templateUrl: "./wallet.component.html",
	styleUrls: ["./wallet.component.scss"],
})
export class WalletComponent implements OnInit {
	CardType = CardType;
	PaymentSystem = PaymentSystem;
	TransferType = TransferType;

	selectedCard: CardDTO;
	cardNumberIsShown: boolean;
	cvvIsShown: boolean;
	cardsIsShown: boolean = true;
	depositsIsShown: boolean;
	transfersIsShown: boolean;

	cards: CardDTO[] = [];
	transfers: TransferDTO[] = [];

	constructor(
		private cardService: CardService,
		private storageService: StorageService,
		private transferService: TransferService
	) {}

	ngOnInit(): void {
		let currentUserId = JSON.parse(
			this.storageService.getItem(
				StorageTypeEnum.LocalStorage,
				Constants.UserIdKey
			)
		);

		this.cardService
			.getCardsForUser(Guid.parse(currentUserId))
			.subscribe((response: any) => {
				this.cards = response;
				this.selectedCard = this.cards[0];
			});
	}

	cardNumberConvertor(cardNumber: string, showCard: boolean) {
		let result = "";

		for (let i = 0; i < cardNumber?.length; i++) {
			if (result.length > 4 && result.length < 20 && showCard) result += "*";
			else result += cardNumber[i];

			if (result.length % 4 == 0) {
				result += "    ";
			}
		}
		return result;
	}

	selectCard(id: Guid) {
		this.cardService.getCard(id).subscribe((response: CardDTO) => {
			this.selectedCard = response;
		});
		this.cvvIsShown = false;
		this.cardNumberIsShown = false;
		this.transfersIsShown = false;
	}

	getTransfers(cardId: number) {
		this.transfersIsShown = !this.transfersIsShown;
		this.transferService
			.getTransfersForCard(cardId)
			.subscribe((response: TransferDTO[]) => {
				this.transfers = response;
			});
	}
}
