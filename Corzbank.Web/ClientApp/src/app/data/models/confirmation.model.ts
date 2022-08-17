import { Guid } from "guid-typescript";
import { VerificationType } from "../enums/verificationType.enum";

export class ConfirmationModel {
  email: string;
  verificationCode: string;
  cardId?: Guid;
  depositId?: Guid;
  verificationType: VerificationType;
}
