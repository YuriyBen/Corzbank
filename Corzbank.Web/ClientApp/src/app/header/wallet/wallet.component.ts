import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Card } from 'src/app/data/entities/card.entity';
import { CardType } from 'src/app/data/enums/card-type.enum';
import { PaymentSystem } from 'src/app/data/enums/payment-system.enum';

@Component({
  selector: 'app-wallet',
  templateUrl: './wallet.component.html',
  styleUrls: ['./wallet.component.scss']
})
export class WalletComponent implements OnInit {

  cards: Card[] = [
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
  ]

  constructor() { }

  ngOnInit(): void {
  }

  cardNumberConvertor(cardNumber: string) {
    let result = "";
    
    for (let i = 0; i < cardNumber.length; i++) {
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

}
