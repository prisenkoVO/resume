import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from '../app-routing.module';


import {
  PreloaderComponent,
  MenuComponent,
  MenuNavContentComponent,
  EmojisComponent,
  PostComponent,
  ToTopButtonComponent,
  LogoutButtonComponent,
  CommentComponent,
  CommentsComponent,
  HeaderComponent,
  FooterComponent,
  ErrorModalComponent,
} from '@shared/components';
import { CreateErrorDirective } from './directives/create-error.directive';

@NgModule({
  declarations: [
    PreloaderComponent,
    MenuComponent,
    MenuNavContentComponent,
    EmojisComponent,
    PostComponent,
    ToTopButtonComponent,
    LogoutButtonComponent,
    CreateErrorDirective,
    CommentsComponent,
    CommentComponent,
    HeaderComponent,
    FooterComponent,
    ErrorModalComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    FormsModule,
  ],
  exports: [
    PreloaderComponent,
    MenuComponent,
    EmojisComponent,
    PostComponent,
    ToTopButtonComponent,
    LogoutButtonComponent,
    HeaderComponent,
    FooterComponent,
    ErrorModalComponent,
  ]
})
export class SharedModule { }
