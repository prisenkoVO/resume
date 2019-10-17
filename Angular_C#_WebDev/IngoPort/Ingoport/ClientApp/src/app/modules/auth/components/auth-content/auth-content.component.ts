import { Component, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-auth-content',
  templateUrl: './auth-content.component.html',
  styleUrls: ['./auth-content.component.scss'],
})
export class AuthContentComponent implements OnInit {

  @Input() showAuthForm: boolean;

  public authAnim = this.showAuthForm ? 'toggleOf' : 'toggleOn';
  public regAnim = this.showAuthForm ? 'toggleOf' : 'toggleOn';

  constructor() { }


  ngOnInit() { }

  ToggleForm() {

    if (this.showAuthForm) {
      this.authAnim = 'toggleOf';
      this.regAnim = 'toggleOn';
    } else {
      this.regAnim = 'toggleOf';
      this.authAnim = 'toggleOn';
    }

    setTimeout(() => {
      const tmp1 = this.authAnim;
      const tmp2 = this.regAnim;

      this.showAuthForm = !this.showAuthForm;

      this.authAnim = tmp1;
      this.regAnim = tmp2;

    }, 350);
    // TODO: do animation here
    // TODO: toggle forms here
  }
}