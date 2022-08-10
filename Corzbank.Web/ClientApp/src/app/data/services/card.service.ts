import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Guid } from "guid-typescript";
import { CardModel } from "../models/card.model";

@Injectable({
  providedIn: "root",
})
export class CardService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_API_URL") private baseUrl: string
  ) {}

  url = this.baseUrl + "cards/";

  getCards() {
    return this.http.get(this.url);
  }

  getCard(id: Guid) {
    return this.http.get(this.url + id);
  }

  getCardsForUser(id: Guid) {
    return this.http.get(this.url + "users/" + id);
  }

  createCard(card: CardModel) {
    return this.http.post(this.url, card);
  }

  deleteCard(id: Guid) {
    return this.http.delete(this.url + id);
  }
}
