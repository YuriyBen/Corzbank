import { Guid } from "guid-typescript";
import { CardType } from "../enums/card-type.enum";
import { PaymentSystem } from "../enums/payment-system.enum";

export class CardModel{
    cardType:CardType;
    paymentSystem:PaymentSystem;
    secretWord:string;
    userId:string;
}