// TODO: TECH DEBT IS
// TODO: remove all HOST in services
// TODO: add index ts to all services

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule,  HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

import { SharedModule } from '@shared/shared.module';
import { AuthModule } from '@modules/auth/auth.module';
import { HomeModule } from '@modules/home/home.module';
import { PersonalareaModule } from '@modules/personalarea/personalarea.module';
import { InternsModule } from '@modules/interns/interns.module';
import { BotModule } from '@modules/bot/bot.module';
import { QAndAModule } from '@modules/q-and-a/q-and-a.module';
import {erorPageModule} from '@modules/404page/errorPage.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    AuthModule,
    HomeModule,
    PersonalareaModule,
    InternsModule,
    BotModule,
    QAndAModule,
    erorPageModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
