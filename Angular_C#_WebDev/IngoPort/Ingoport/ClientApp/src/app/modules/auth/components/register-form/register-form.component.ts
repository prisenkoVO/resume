import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';
import { User } from '@app/shared/interfaces/iUser';
import { AuthService } from '@app/core/auth/auth.service';
import { UtilityService } from '@app/core/utility/utility.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss'],
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


export class RegisterFormComponent implements OnInit {

  @Input() showRegister: boolean;
  @Input() animateState: string;
  @Output() ToggleForm = new EventEmitter<boolean>();

  private roleCheckboxes = {
    mentorRoleCheck: false,
    internRoleCheck: false,
    moderatorRoleCheck: false,
  };

  public user: User = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    roleId: 3, // TODO: refactor this component
  };


  constructor(
    private auth: AuthService,
    private utility: UtilityService,
    private router: Router,
  ) {}

  ngOnInit() {
  }


  registerUser() {
    this.auth.registerUser(this.user)
      .toPromise()
      .then(res => {
        document.cookie = `name=${res.name}`;
        document.cookie = `token=${res.token}`;
        this.router.navigateByUrl('');
      })
      .catch(err => this.utility.handleHttpError(err))
  }

  toggleForm() {
    this.ToggleForm.emit();
  }

}