import { Component, OnInit, Input } from '@angular/core';
import { Team } from '@app/shared/interfaces/iTeam';

@Component({
  selector: 'app-user-team',
  templateUrl: './user-team.component.html',
  styleUrls: ['./user-team.component.scss']
})
export class UserTeamComponent implements OnInit {

  @Input() Team: Team[];
  @Input() TeamName:string;

  constructor() { }


  ngOnInit() {
  }

}
