import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import {
  // slider,
  // transformer,
  fader
  // stepper
} from './route-animations';
import { UtilityService } from '@core/utility/utility.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    fader,
  ]
})
export class AppComponent {

  constructor(
    private utility: UtilityService,
  ) {
  }

  public _httpError = false;
  public _httpErrorMessage: string;
  public _httpErrorCode: number;

  public mainBgColor = false;
 
  ngOnInit() {
    this.utility.toggleErrorModal.subscribe((err) => {
      this._httpErrorMessage = err.message;
      this._httpErrorCode = err.status;
 
      this._httpError = true;
    });

    console.log('Hello');
  }

  // Change styles theme
  switchTheme(theme) {
    document.documentElement.setAttribute('data-theme', `${theme}`);
  }

  closeModalError(){
    this._httpError = false;
  }

  prepareRoute(outlet: RouterOutlet) {
    this.checkForAuth(outlet);
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }

  checkForAuth(route: RouterOutlet) {
    this.mainBgColor = window.location.pathname.startsWith('/auth');
  }
}
