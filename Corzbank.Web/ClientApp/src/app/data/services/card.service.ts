import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { Observable, Subject } from "rxjs";
import { Card } from "../dtos/card.dto";
import { CardModel } from "../models/card.model";

@Injectable({
	providedIn: "root",
})
export class CardService {
	cardSubject = new Subject();

	url = this.baseUrl + "cards/";

	constructor(
		private http: HttpClient,
		@Inject("BASE_API_URL") private baseUrl: string
	) {}

	getCards(): Observable<Card[]> {
		return this.http.get<Card[]>(this.url);
	}

	getCard(id: Guid): Observable<Card> {
		return this.http.get<Card>(this.url + id);
	}

	getCardsForUser(id: Guid): Observable<Card[]> {
		return this.http.get<Card[]>(this.url + "users/" + id);
	}

	createCard(card: CardModel): Observable<Card> {
		return this.http.post<Card>(this.url, card);
	}

	deleteCard(id: Guid): Observable<boolean> {
		return this.http.delete<boolean>(this.url + id);
	}
}
