import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  @Output() SwitchTheme = new EventEmitter();

  private _theme = 'light';
  public _themeIconClass = 'fa-sun'; // can check for fa-moon from font-awesome

  ngOnInit() {
    this.avatar = window.localStorage.getItem('photo') || 'assets/images/female.png';

    console.log(this.avatar);
  }

  public avatar = null || 'assets/images/female.png'


  switchTheme() {
    if (this._theme === 'light') {
      this._theme = 'dark';
      this._themeIconClass = 'fa-moon';
    } else {
      this._theme = 'light';
      this._themeIconClass = 'fa-sun';
    }

    this.SwitchTheme.emit(this._theme);
  }

}
