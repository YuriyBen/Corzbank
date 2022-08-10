import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { TransferModel } from "../models/transfer.model";

@Injectable({
  providedIn: "root",
})
export class TransferService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_API_URL") private baseUrl: string
  ) {}

  url = this.baseUrl + "transfers/";

  getTransfers() {
    return this.http.get(this.url);
  }

  getTransfer(id: Guid) {
    return this.http.get(this.url + id);
  }

  getTransfersForCard(cardId: number) {
    return this.http.get(this.url + "cards/" + cardId);
  }

  createTransfer(transfer: TransferModel) {
    return this.http.post(this.url, transfer);
  }
}
