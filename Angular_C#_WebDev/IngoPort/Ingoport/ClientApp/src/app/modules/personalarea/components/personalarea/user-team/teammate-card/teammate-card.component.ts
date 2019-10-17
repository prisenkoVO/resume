import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-teammate-card',
  templateUrl: './teammate-card.component.html',
  styleUrls: ['./teammate-card.component.scss']
})
export class TeammateCardComponent implements OnInit {

  constructor() { }
  
  @Input() FirstName: string;
  @Input() LastName: string;
  @Input() RoleId: number;
  @Input() Photo: string;
  
  ngOnInit() {}

  getPhoto(){
    if(this.Photo == null){
      return "assets/images/icon-user-default.png";
    }
    else{
      return this.Photo;
    }
  }

}
