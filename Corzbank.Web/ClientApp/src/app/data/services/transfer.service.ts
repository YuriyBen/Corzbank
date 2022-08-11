import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { Observable } from "rxjs";
import { Transfer } from "../dtos/transfer.dto";
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

	getTransfers(): Observable<Transfer[]> {
		return this.http.get<Transfer[]>(this.url);
	}

	getTransfer(id: Guid): Observable<Transfer> {
		return this.http.get<Transfer>(this.url + id);
	}

	getTransfersForCard(cardId: Guid): Observable<Transfer[]> {
		return this.http.get<Transfer[]>(this.url + "cards/" + cardId);
	}

	createTransfer(transfer: TransferModel): Observable<Transfer> {
		return this.http.post<Transfer>(this.url, transfer);
	}
}
