// TODO: refactor it for NG animations
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {


  constructor() { }

  public _showMenu = false;

  ngOnInit() {
  }

  toggleMenu() {
    this._showMenu = !this._showMenu;
  }
}