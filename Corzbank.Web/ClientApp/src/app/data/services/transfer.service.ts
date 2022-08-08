import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { Constants } from "../helpers/constants";
import { TransferModel } from "../models/transfer.model";

@Injectable({
    providedIn: 'root',
})

export class TransferService {

    constructor(private http: HttpClient) { }

    url = Constants.ServerUrl + 'transfers/';

    getTransfers() {
        return this.http.get(this.url)
    }

    getTransfer(id: Guid) {
        return this.http.get(this.url + id)
    }

    getTransfersForCard(cardId: number) {
        return this.http.get(this.url + 'cards/' + cardId)
    }

    createTransfer(transfer:TransferModel){
        return this.http.post(this.url, transfer)
    }
}