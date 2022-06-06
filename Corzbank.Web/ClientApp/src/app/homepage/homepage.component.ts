import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {
  creditCard = "0000000000000000"
  phoneNumber = "123456789"

  constructor() { }
  ngOnInit() {
  }

}
  