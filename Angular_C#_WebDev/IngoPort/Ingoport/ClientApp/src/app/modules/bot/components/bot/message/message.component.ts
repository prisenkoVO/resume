import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { Message } from '@app/shared/interfaces/iMessage';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  constructor() { }

  @ViewChild('message', {static: false}) _message: ElementRef;

  @Input() Message: Message;


  ngOnInit() {
    
  }

  ngAfterViewInit(): void {
    this._message.nativeElement.innerHTML = this.Message.text.replace(/\\n/g, '\n');
  }
}
