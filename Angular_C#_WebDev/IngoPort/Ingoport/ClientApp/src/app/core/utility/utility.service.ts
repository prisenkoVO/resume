import {
  Injectable,
  Inject
} from '@angular/core';
import { EventEmitter } from '@angular/core';
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {
  constructor(
    @Inject('BASE_URL') HOST: string,
    private router: Router,
  ) {
    this.host = HOST;
  }

  public host: string = null;

  public toggleErrorModal = new EventEmitter();

  deleteCookie(name: string): void {
    document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
  }

  getCookie(name: string): string {
    const matches = document.cookie.match(new RegExp(
      '(?:^|; )' + name.replace(/([.$?*|{}()[]\\\/\+^])/g, '\\$1') + '=([^;]*)'
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
  }

  getAuthToken(): string {
    return 'Bearer' + ' ' + this.getCookie('token');
  }

  getCurrentDate(): string {
    const date = new Date();

    const currentDay = date.getDate();
    const currentMonth = date.getMonth() + 1;
    const currentYear = date.getFullYear();

    const currentHour = date.getHours();
    const currentMinute = date.getMinutes();
    const currentSeconds = date.getSeconds();
    return `${currentYear}-${currentMonth}-${currentDay+1}`;
  }

  setHttpHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-type': 'application/json',
      'Authorization': this.getAuthToken(),
    });
  }


  handleHttpError(err: HttpErrorResponse) {
    this.toggleErrorModal.emit(err);

    // TODO: delete all navigates in another files
    if (err.status === 401) {
      this.router.navigateByUrl('/auth');
    }
  }


}
