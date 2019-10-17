import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { RandomCoffeeService } from '@core/randomCoffee/random-coffee.service';
import { UtilityService } from '@app/core/utility/utility.service';
import { FeedBack } from '@app/shared/interfaces/iFeedBack';

@Component({
  selector: 'app-message-input',
  templateUrl: './message-input.component.html',
  styleUrls: ['./message-input.component.scss']
})
export class MessageInputComponent implements OnInit {
  constructor(
    private coffee: RandomCoffeeService,
    private utility: UtilityService,
  ) {}

  private _coffeeStatus = 0;
  @Input()
  get coffeeStatus(): number {
    return this._coffeeStatus;
  }
  set coffeeStatus(value: number) {
    this._coffeeStatus = value || 0;
    this.coffeeBgColor = this.coffeeColors[this._coffeeStatus || 0];
  }

  @Input() showCoffeeButtons: boolean;

  @Output() sendMessage = new EventEmitter();
  @Output() coffeeAction = new EventEmitter();
  @Output() toggleCoffeeButtons = new EventEmitter();

  public messageText: string;

  private coffeeColors = ['black', 'red', 'green', 'yellow'];

  public coffeeBgColor = 'black';

  public coffeeMessage: string = null;

  public enrollCoffeeStatuses = [0, 4];
  public refuseCoffeeStatuses = [1, 3];
  public currentPairCoffeeStatuses = [2];

  public partnerFeedback: string = null;
  public isFeedbackFieldToggled = false;
  public feedbackStar = 1;

  ngOnInit() {
    this.coffee.getCoffeeStatus()
    .subscribe(
      res => this.coffeeStatus = res,
      err => this.utility.handleHttpError(err),
    );
  }

  onSendMessage() {
    if (this.messageText.length >= 1) {
      this.sendMessage.emit(this.messageText);
      this.messageText = '';
    }
  }

  onToggleCoffeeButtons() {
    this.toggleCoffeeButtons.emit();
  }

  enrollRandomCoffee() {
    const isActionAllowed = this.checkCoffeeAction(this.enrollCoffeeStatuses);
    if (isActionAllowed) {
      this.coffee.enrollRandomCoffee()
      .subscribe(
        res => {
          this.coffeeMessage = res.message;
          this.coffeeStatus = 1;
        },
        err => this.utility.handleHttpError(err),
      );
      return;
    }

    this.coffeeMessage = 'Сейчас вы не можете найти собеседника';
  }

  getRcHistory() {
    this.coffee.getCoffeeHistory()
      .subscribe(
        res => console.log(res.text()),
        err => this.utility.handleHttpError(err),
      );
  }

  refuseCoffee() {
    const isActionAllowed = this.checkCoffeeAction(this.refuseCoffeeStatuses);
    if (isActionAllowed) {
      this.coffee.refuseRandomCoffee()
      .subscribe(
        res => {
          this.coffeeMessage = res.message;
          this.coffeeStatus = 3;
        },
        err => this.utility.handleHttpError(err),
      );
      return;
    }

    this.coffeeMessage = 'Сейчас вы не можете отменить встречу';
  }

  getCurrentPair() {
    this.coffee.getCoffeePair()
      .subscribe(
        res => {
          this.coffeeMessage = `Ваш собеседник ${res[0].Name}`;
        },
        err => this.utility.handleHttpError(err),
      )

    // this.coffeeMessage = 'Сейчас вы не можете узнать текущего собеседника';
  }

  leaveFeedback() {
    const FeedBack: FeedBack = {
      Star: this.feedbackStar,
      Feedback: this.partnerFeedback,
    };

    this.coffee.leaveFeedback(FeedBack)
      .subscribe(
        res => {
          console.log(res)
        },
        err => this.utility.handleHttpError(err),
      )
  }

  toggleFeedbackField() {
    this.isFeedbackFieldToggled = !this.isFeedbackFieldToggled;
  }

  // TODO: rename this method
  checkCoffeeAction(allowedStatuses: number[], getStyles?: boolean) {
    console.log(this.coffeeStatus);
    const result = allowedStatuses.indexOf(this.coffeeStatus) > -1;
    return result;
  }
}
