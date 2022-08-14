import { Component, Inject, OnInit } from "@angular/core";
import {
	FormGroup,
	FormControl,
	Validators,
	FormBuilder,
} from "@angular/forms";
import { Guid } from "guid-typescript";
import { Card } from "src/app/data/dtos/card.dto";
import { Deposit } from "src/app/data/dtos/deposit.dto";
import { Transfer } from "src/app/data/dtos/transfer.dto";
import { CardType } from "src/app/data/enums/card-type.enum";
import { PaymentSystem } from "src/app/data/enums/payment-system.enum";
import { StorageTypeEnum } from "src/app/data/enums/storage-type.enum";
import { TransferType } from "src/app/data/enums/transfer-type.enum";
import { Constants } from "src/app/data/helpers/constants";
import { DepositModel } from "src/app/data/models/deposit.model";
import { TransferModel } from "src/app/data/models/transfer.model";
import { CardService } from "src/app/data/services/card.service";
import { DepositService } from "src/app/data/services/deposit.service";
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
	cardTypeKeys = Object.keys(this.CardType).filter((k) => !isNaN(Number(k)));
	paymentSystemKeys = Object.keys(this.PaymentSystem).filter(
		(k) => !isNaN(Number(k))
	);

	selectedCard: Card;
	selectedDeposit: Deposit;
	cardDataIsDisplayed: boolean;
	cardNumberIsDisplayed: boolean;
	cvvIsDisplayed: boolean;
	cardsIsDisplayed: boolean = true;
	depositsIsDisplayed: boolean;
	transactionsIsDisplayed: boolean;
	transferMenuIsDisplayed: boolean;
	settingsIsDisplayed: boolean;
	openCardMenuIsDisplayed: boolean;
	openDepositMenuIsDisplayed: boolean;
	depositDataIsDisplayed:boolean;
	currentUserId: Guid;
	durationOfDeposit: number = 1;
	amountOfDeposit: number = 1;
	profitForDeposit: number = 1;
	cardForDeposit: Card;

	cards: Card[] = [];
	transfers: Transfer[] = [];
	deposits: Deposit[] = [];

	transferForm: FormGroup;
	createCardForm: FormGroup;

	constructor(
		private cardService: CardService,
		private storageService: StorageService,
		private transferService: TransferService,
		private notificationService: NotificationService,
		private depositService: DepositService
	) { }

	get receiverCardNumber() {
		return this.transferForm.get("receiverCardNumber");
	}

	get note() {
		return this.transferForm.get("note");
	}

	get amount() {
		return this.transferForm.get("amount");
	}

	get cardType() {
		return this.createCardForm.get("cardType");
	}

	get paymentSystem() {
		return this.createCardForm.get("paymentSystem");
	}

	get secretWord() {
		return this.createCardForm.get("secretWord");
	}

	ngOnInit(): void {
		this.cardService.cardSubject.subscribe((data: any) => {
			this.cards = data;
		});

		this.currentUserId = JSON.parse(
			this.storageService.getItem(
				StorageTypeEnum.LocalStorage,
				Constants.UserIdKey
			)
		);

		this.getCards();

		this.getDeposits();

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

		this.createCardForm = new FormGroup({
			cardType: new FormControl("", Validators.required),
			paymentSystem: new FormControl("", Validators.required),
			secretWord: new FormControl("", [
				Validators.required,
				Validators.minLength(3),
				Validators.maxLength(20),
				Validators.pattern("[a-zA-Z]+"),
			]),
		});
	}

	getCards() {
		if (this.currentUserId != null) {
			this.cardService
				.getCardsForUser(Guid.parse(this.currentUserId.toString()))
				.subscribe((response: any) => {
					this.cardService.cardSubject.next(response);
				});
		}
	}

	getDeposits() {
		this.depositService
			.getDepositsForUser(Guid.parse(this.currentUserId.toString()))
			.subscribe((deposits: any) => {
				this.deposits = deposits;
				console.log(deposits);
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
		this.hideAllMenus();
		this.cardDataIsDisplayed = true;
		this.depositDataIsDisplayed = false;
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

	openSettings() {
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

	openCardMenu() {
		this.hideAllMenus();
		this.cardDataIsDisplayed = false;
		this.openCardMenuIsDisplayed = !this.openCardMenuIsDisplayed;
	}

	openDepositMenu() {
		this.hideAllMenus();
		this.cardDataIsDisplayed = false;
		this.openDepositMenuIsDisplayed = !this.openDepositMenuIsDisplayed;
		this.cardForDeposit = this.cards[0];
	}

	createCard() {
		const cardForCreatingForm = this.createCardForm.value;
		cardForCreatingForm.userId = this.currentUserId;
		this.cardService.createCard(this.createCardForm.value).subscribe(
			(card) => {
				console.log(card);
				if (card === null) {
					this.notificationService.showErrorNotification(
						"This type of card already exists",
						""
					);
				} else {
					this.cards.push(card);
					this.notificationService.showSuccessfulNotification(
						"Card was successfuly created",
						""
					);
				}
			},
			(error) => {
				this.notificationService.showErrorNotification("Error", "");
			}
		);
		this.createCardForm.reset();
	}

	increment() {
		if (this.durationOfDeposit < 24) this.durationOfDeposit += 1;
	}
	decrement() {
		if (this.durationOfDeposit > 1) this.durationOfDeposit -= 1;
	}

	openDeposit() {
		var depositModel: DepositModel = {
			cardId: this.cardForDeposit.id,
			amount: this.amountOfDeposit,
			duration: this.durationOfDeposit,
		};

		this.depositService
			.openDeposit(depositModel)
			.subscribe((deposit: Deposit) => {
				console.log(deposit);
				this.deposits.push(deposit);
			});
		this.amountOfDeposit = 1;
		this.durationOfDeposit = 1;
	}

	selectDeposit(id: Guid) {
		this.depositService.getDeposit(id).subscribe((response: Deposit) => {
			this.selectedDeposit = response;
		});
		this.hideAllMenus();
		this.depositDataIsDisplayed = true;
		this.cardDataIsDisplayed = false;
	}

	hideAllMenus() {
		this.transactionsIsDisplayed = false;
		this.transferMenuIsDisplayed = false;
		this.transactionsIsDisplayed = false;
		this.cvvIsDisplayed = false;
		this.cardNumberIsDisplayed = false;
		this.transactionsIsDisplayed = false;
		this.settingsIsDisplayed = false;
		this.openCardMenuIsDisplayed = false;
		this.openDepositMenuIsDisplayed = false;
	}
}
