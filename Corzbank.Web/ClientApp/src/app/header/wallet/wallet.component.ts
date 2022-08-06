import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { User } from 'oidc-client';
import { Card } from 'src/app/data/entities/card.entity';
import { CardType } from 'src/app/data/enums/card-type.enum';
import { PaymentSystem } from 'src/app/data/enums/payment-system.enum';
import { StorageTypeEnum } from 'src/app/data/enums/storage-type.enum';
import { Constants } from 'src/app/data/helpers/constants';
import { CardService } from 'src/app/data/services/card.service';
import { StorageService } from 'src/app/data/services/storage.service';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent implements OnInit {
  CardType = CardType;
  PaymentSystem = PaymentSystem;
  selectedCard: Card;

  cards: Card[]=[
    {
      id: Guid.create(),
      cardNumber: "1234567891023456",
      expirationDate: "08/24",
      cvvCode: "333",
      cardType: CardType.Credit,
      paymentSystem: PaymentSystem.Mastercard,
      balance: 125,
      isActive: true,
      secretWord: "IVAN",
      userId: Guid.create()
    },
    {
      id: Guid.create(),
      cardNumber: "7896547896541236",
      expirationDate: "08/24",
      cvvCode: "666",
      cardType: CardType.Debit,
      paymentSystem: PaymentSystem.Visa,
      balance: 48651,
      isActive: true,
      secretWord: "IVAN",
      userId: Guid.create()
    },
    {
      id: Guid.create(),
      cardNumber: "7896547896541236",
      expirationDate: "08/24",
      cvvCode: "666",
      cardType: CardType.Universal,
      paymentSystem: PaymentSystem.Visa,
      balance: 48651,
      isActive: true,
      secretWord: "IVAN",
      userId: Guid.create()
    }
  ];

  constructor(private cardService: CardService, private storageService: StorageService) { }

  ngOnInit(): void {    
    let currentUserId = JSON.parse(this.storageService.getItem(StorageTypeEnum.LocalStorage, Constants.UserIdKey));

    this.cardService.getCardsForUser(Guid.parse(currentUserId)).subscribe((response: any) => {
      this.cards = response;
      this.selectedCard = this.cards[0];

    })
  }

  cardNumberConvertor(cardNumber: string) {
    let result = "";

    for (let i = 0; i < cardNumber?.length; i++) {
      if (result.length > 4 && result.length < 20)
        result += '*';
      else
        result += cardNumber[i];

      if (result.length % 4 == 0) {
        result += "    ";
      }
    };
    return result;
  }

  selectCard(id: Guid) {
    this.cardService.getCard(id).subscribe((response:Card)=>{
      this.selectedCard = response;
    })
  }
}
