import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { 
  BotComponent,
  MessageComponent,
  MessageInputComponent,
 } from '@modules/bot/components/';





@NgModule({
  declarations: [BotComponent, MessageComponent, MessageInputComponent],
  imports: [
    CommonModule,
    FormsModule,
  ]
})
export class BotModule { }
