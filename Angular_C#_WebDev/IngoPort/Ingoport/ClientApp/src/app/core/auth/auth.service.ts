import { Injectable } from '@angular/core';

import { User } from '@app/shared/interfaces/iUser';
import { AuthToken } from '@shared/interfaces/iAuthToken';

import { HttpClient } from '@angular/common/http';
import { UtilityService } from '@core/utility/utility.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private utility: UtilityService) { }

  private readonly HOST = this.utility.host;

  auth(user: User): Observable<AuthToken> {
    return this.http.post<AuthToken>(`${this.HOST}/api/auth/login`, user, {
      headers: {
        'Content-type': 'application/json'
      }
    })
  }

  registerUser(user: User): Observable<AuthToken> {
    return this.http.post<AuthToken>(`${this.HOST}/api/reg`, user, {
      headers: {
        'Content-type': 'application/json'
      }
    });
  }
}
