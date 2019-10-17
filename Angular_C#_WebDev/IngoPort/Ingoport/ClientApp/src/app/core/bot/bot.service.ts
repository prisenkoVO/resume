import { Injectable } from '@angular/core';

import { UtilityService } from '@core/utility/utility.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BotService {

  constructor(
    private utility: UtilityService,
    private http: HttpClient,
  ) { }

  private readonly HOST = this.utility.host;

  // http://localhost:54677/api/bot/?query=Привет

  checkForAuth(): Observable<string> {
    return this.http.get<string>(`${this.HOST}/api/bot/`, {
      params: {
        query: 'Hello',
      },
      headers: this.utility.setHttpHeaders(),
    });
  }

  sendMessage(message: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.HOST}/api/bot/?query=${message}`, {
      headers: this.utility.setHttpHeaders(),
    })
  }
}
