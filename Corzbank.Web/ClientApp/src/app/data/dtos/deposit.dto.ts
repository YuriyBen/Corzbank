import { Guid } from "guid-typescript";
import { DepositStatus } from "../enums/deposit-status.enum";

export class Deposit {
	id: Guid;
	amount: number;
	duration: number;
	apy: number;
	createdDate: Date;
	expirationDate: Date;
	profit: number;
	status: DepositStatus;
	cardId: Guid;
}
