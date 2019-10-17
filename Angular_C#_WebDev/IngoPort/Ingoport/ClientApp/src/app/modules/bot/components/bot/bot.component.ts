// TODO: refactor renadom coffee
// TODO: add modalwindow for success
// TODO: add service for modal windows
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

import { UtilityService } from '@core/utility/utility.service';
import { BotService } from '@core/bot/bot.service';
import { RandomCoffeeService } from '@core/randomCoffee/random-coffee.service';

import { Message } from '@shared/interfaces/iMessage';


@Component({
  selector: 'app-bot',
  templateUrl: './bot.component.html',
  styleUrls: ['./bot.component.scss']
})
export class BotComponent implements OnInit {

  constructor(
    private utility: UtilityService,
    private bot: BotService,
    private router: Router,
    private coffee: RandomCoffeeService,
  ) { }

  public messages: Message[] = [];
  public messageText: string;

  public coffeeStatus: number;

  public showCoffeeButtons = false;

  ngOnInit() {}

  sendMessage(text: string){
    this.messageText = '';
    const clientMessage: Message = {
      text,
      isClientMessage: true,
      isLastMessage: false,
    };
    this.messages = [...this.messages, clientMessage]

    this.bot.sendMessage(text)
      .subscribe(
        res => {
          const botMessages: Message[] = [];
          console.log(res);
          if (res === null || res.length < 1) {
            const botMessage: Message = {
              text: 'Простите, я не могу ответить',
              isClientMessage: false,
              isLastMessage: true,
            };

            this.messages = [...this.messages, botMessage];
          } else {
            res.forEach(message => {

              const botMessage: Message = {
                text: message,
                isClientMessage: false,
                isLastMessage: true,
              };

              botMessages.push(botMessage);
            });

            this.messages = [...this.messages, ...botMessages]
          }

        },
        err => this.utility.handleHttpError(err)
      );
  }

  toggleCoffeeButtons() {
    this.showCoffeeButtons = !this.showCoffeeButtons;
    this.messages = [];
    console.log(this.messages)
    console.log(`CoffeButtons ${this.showCoffeeButtons}`);
  }
}
