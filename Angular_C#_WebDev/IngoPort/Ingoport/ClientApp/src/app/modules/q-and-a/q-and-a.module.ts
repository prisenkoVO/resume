import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';


import {
  QnaPageComponent,
  TopicComponent
} from '@modules/q-and-a/components';

@NgModule({
  declarations: [
    QnaPageComponent,
    TopicComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
  ]
})
export class QAndAModule { }
