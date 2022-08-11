import { Guid } from "guid-typescript";
import { TransferType } from "../enums/transfer-type.enum";

export class Transfer {
	id: Guid;
	transferType: TransferType;
	receiverPhoneNumber: string;
	amount: number;
	date: Date;
	note: string;
	isSuccessful: boolean;
	senderCardId: Guid;
	receiverCardId?: Guid;
}
