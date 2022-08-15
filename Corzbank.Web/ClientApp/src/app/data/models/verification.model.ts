import { Guid } from "guid-typescript";
import { VerificationType } from "../enums/verificationType.enum";

export class VerificationModel {
  email?: string;
  verificationType: VerificationType;
  cardId?: Guid;
  depositId?: Guid;
}
