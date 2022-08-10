import { Guid } from "guid-typescript";
import { TransferType } from "../enums/transfer-type.enum";

export class TransferModel {
  transferType: TransferType;
  receiverPhoneNumber: string;
  amount: number;
  note: string;
  senderCardId: Guid;
  receiverCardId?: Guid;
}
