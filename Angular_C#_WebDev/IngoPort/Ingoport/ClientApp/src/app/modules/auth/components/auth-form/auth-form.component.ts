
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';

import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

import { User } from '@app/shared/interfaces/iUser';
import { AuthService } from '@core/auth/auth.service';
import { UtilityService } from '@core/utility/utility.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-form',
  templateUrl: './auth-form.component.html',
  styleUrls: ['./auth-form.component.scss'],
  animations: [
    trigger('changeState', [
      state('toggleOn', style({
        transform: 'scale(1)',
      })),
      state('toggleOf', style({
        transform: 'scale(0)',
      })),
      transition('*=>toggleOn', animate('300ms')),
      transition('*=>toggleOf', animate('300ms'))
    ])
  ],
})
export class AuthFormComponent implements OnInit {

  //
  @Input() showAuth: boolean;
  @Input() animateState: string;

  @Output() ToggleForm = new EventEmitter<boolean>();
  // @Output() auth = new EventEmitter<User>();

  @ViewChild('authButton', { read: false, static: true }) authButton;

  // Fields of the auth form
  public _emailInput: string;
  public _passwordInput: string;
  public remember: boolean = this.utility.getCookie('remember') === 'true';


  constructor(
    private authService: AuthService,
    private utility: UtilityService,
    private router: Router,
  ) { }

  ngOnInit() {}

  setPass(password) {
    this._emailInput = password;
  }

  setEmail(email) {
    this._emailInput = email;
  }

  auth() {
    const user: User = {
      email: null,
      password: null,
    };



    // TODO: replace for private method
    if (this.utility.getCookie('email') !== undefined) {
      user.email = this.utility.getCookie('email');
      user.password = this.utility.getCookie('password');
    } else {
      user.email = this._emailInput;
      user.password = this._passwordInput;
    }

    this.authService.auth(user)
      .subscribe(
        res => {
          window.localStorage.setItem('auth', 'true');
          document.cookie = `token=${res.token}`;
          document.cookie = `name=${res.name}`;
          window.localStorage.setItem('photo', `${res.photo}`);

          this.router.navigateByUrl('/news');
        },
        err => {
          this.utility.handleHttpError(err);
        }
      );

    this.utility.deleteCookie('email');
    this.utility.deleteCookie('password');

    if (this.remember) {
      document.cookie = `login=${this._emailInput}`;
      document.cookie = `password=${this._passwordInput}`;
      document.cookie = `remember=true`;
    }

  }

  // Toggle forms on auth page
  toggleForm($event: MouseEvent) {
    $event.preventDefault();
    this.ToggleForm.emit();
  }

  validatePassword() {
    const whiteSpaces = this._passwordInput.indexOf(' ');
    console.log(whiteSpaces);
    const length = this._passwordInput.length >= 6;
    return whiteSpaces && length;
  }

  roleOnCheck(event: Event) {

  }
}
