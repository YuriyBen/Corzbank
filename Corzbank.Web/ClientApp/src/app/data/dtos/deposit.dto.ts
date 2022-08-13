import { Guid } from "guid-typescript";
import { DepositStatus } from "../enums/deposit-status.enum";

export class Deposit {
	id: Guid;
	amount: number;
	duration: number;
	apy: number;
	openDate: Date;
	endDate: Date;
	profit: number;
	status: DepositStatus;
	CardId: Guid;
}
