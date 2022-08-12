import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Guid } from "guid-typescript";
import { Card } from "src/app/data/dtos/card.dto";
import { Transfer } from "src/app/data/dtos/transfer.dto";
import { CardType } from "src/app/data/enums/card-type.enum";
import { PaymentSystem } from "src/app/data/enums/payment-system.enum";
import { StorageTypeEnum } from "src/app/data/enums/storage-type.enum";
import { TransferType } from "src/app/data/enums/transfer-type.enum";
import { Constants } from "src/app/data/helpers/constants";
import { TransferModel } from "src/app/data/models/transfer.model";
import { CardService } from "src/app/data/services/card.service";
import { NotificationService } from "src/app/data/services/notification.service";
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

	selectedCard: Card;
	cardDataIsDisplayed: boolean;
	cardNumberIsDisplayed: boolean;
	cvvIsDisplayed: boolean;
	cardsIsDisplayed: boolean = true;
	depositsIsDisplayed: boolean;
	transactionsIsDisplayed: boolean;
	transferMenuIsDisplayed: boolean;
	settingsIsDisplayed: boolean;

	cards: Card[] = [];
	transfers: Transfer[] = [];

	transferForm: FormGroup;

	constructor(
		private cardService: CardService,
		private storageService: StorageService,
		private transferService: TransferService,
		private notificationService: NotificationService
	) {}

	get receiverCardNumber() {
		return this.transferForm.get("receiverCardNumber");
	}

	get note() {
		return this.transferForm.get("note");
	}

	get amount() {
		return this.transferForm.get("amount");
	}

	ngOnInit(): void {
		this.cardService.cardSubject.subscribe((data: any) => {
			this.cards = data;
		});

		this.getCards();

		this.transferForm = new FormGroup({
			receiverCardNumber: new FormControl("", [
				Validators.required,
				Validators.minLength(16),
				Validators.maxLength(16),
				Validators.pattern("[0-9 ]+"),
			]),
			note: new FormControl("", [Validators.maxLength(100)]),
			amount: new FormControl("", [
				Validators.required,
				Validators.pattern("^[0-9]+([,.][0-9]+)?$"),
			]),
		});
	}

	getCards() {
		let currentUserId = JSON.parse(
			this.storageService.getItem(
				StorageTypeEnum.LocalStorage,
				Constants.UserIdKey
			)
		);

		this.cardService
			.getCardsForUser(Guid.parse(currentUserId))
			.subscribe((response: any) => {
				this.cardService.cardSubject.next(response);
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
		this.cardService.getCard(id).subscribe((response: Card) => {
			this.selectedCard = response;
		});
		this.cardDataIsDisplayed = true;
		this.hideAllMenus();
	}

	getTransfers() {
		this.hideAllMenus();
		this.transactionsIsDisplayed = !this.transactionsIsDisplayed;
		this.transferService
			.getTransfersForCard(this.selectedCard.id)
			.subscribe((response: Transfer[]) => {
				this.transfers = response;
			});
	}

	openTransferMenu() {
		this.hideAllMenus();
		this.transferMenuIsDisplayed = !this.transferMenuIsDisplayed;
	}

	openSettings(){
		this.hideAllMenus();
		this.settingsIsDisplayed = !this.settingsIsDisplayed;
	}

	sendTransfer() {
		var transferModel: TransferModel = {
			transferType: TransferType.Card,
			amount: this.transferForm.value.amount,
			note: this.transferForm.value.note,
			receiverCardNumber: this.transferForm.value.receiverCardNumber,
			senderCardId: this.selectedCard.id,
			receiverPhoneNumber: "",
		};

		this.transferService
			.createTransfer(transferModel)
			.subscribe((data: any) => {
				if (data === null) {
					this.notificationService.showErrorNotification(
						"Invalid data",
						""
					);
				} else {
					this.notificationService.showSuccessfulNotification(
						"Transfer was successfuly delivered",
						""
					);

					this.transferForm.reset();
					this.getCards();
					this.selectCard(this.selectedCard.id);
				}
			});
	}

	hideAllMenus() {
		this.transactionsIsDisplayed = false;
		this.transferMenuIsDisplayed = false;
		this.transactionsIsDisplayed = false;
		this.cvvIsDisplayed = false;
		this.cardNumberIsDisplayed = false;
		this.transactionsIsDisplayed = false;
		this.settingsIsDisplayed = false;
	}
}
