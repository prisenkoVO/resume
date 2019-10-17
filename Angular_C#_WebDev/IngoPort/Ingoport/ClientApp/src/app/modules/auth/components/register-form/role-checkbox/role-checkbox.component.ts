import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-role-checkbox',
  templateUrl: './role-checkbox.component.html',
  styleUrls: ['./role-checkbox.component.scss']
})
export class RoleCheckboxComponent implements OnInit {


  @Input() name: string;
  @Input() Id: number;
  @Input() checked: boolean;

  @Output() onCheck = new EventEmitter();

  constructor() { }
  
  ngOnInit() {
  }

  toggleCheckbox(id) {
    this.onCheck.emit(id);
  }

}
