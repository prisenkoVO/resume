import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {

  constructor() { }

  @Input() FirstName: string; 
  @Input() LastName: string;
  @Input() RoleId: number;
  
  ngOnInit() {
  }

}
