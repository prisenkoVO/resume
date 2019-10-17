import { Component, OnInit, Input } from '@angular/core';

import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-menu-nav-content',
  templateUrl: './menu-nav-content.component.html',
  styleUrls: ['./menu-nav-content.component.scss'],
  // animations: [
  //   trigger('playBox',[
  //     state('closed',style({
  //     width: '5%',
  //     backgroundColor:'red',
  //     transform:'translateX(0)'
  //   })),
  //   state('open',style({
  //         visibility: 'visible',
  //         opacity: '1',
  //         transition: '0s linear'
  //   })),transition('closed => open', animate(1000))])
  // ]
})
export class MenuNavContentComponent implements OnInit {

  constructor() { }

  @Input() isActive: boolean;

  private _state = 'closed';
  private _navContentIsActive = false;

  ngOnInit() {
    this._state = 'open';
    this._navContentIsActive = true;
  }


  closeMenu() {
    // TODO: animate on destroy here
  }
}
