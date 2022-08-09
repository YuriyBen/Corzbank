import { Guid } from "guid-typescript";
import { CardType } from "../enums/card-type.enum";
import { PaymentSystem } from "../enums/payment-system.enum";

export class Card {
    id: Guid;
    cardNumber: string;
    expirationDate: string;
    cvvCode: string;
    cardType: CardType;
    paymentSystem: PaymentSystem;
    balance: number;
    isActive: boolean;
    secretWord: string;
    userId: any;
}