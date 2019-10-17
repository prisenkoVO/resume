import { Component, OnInit, Input } from '@angular/core';
import { Personalarea } from '@app/shared/interfaces/iPersonalarea';
import { Tasks } from '@app/shared/interfaces/iTasks';
import { Team } from '@app/shared/interfaces/iTeam';
import { PersonalareaService } from '@app/core/personalarea/personalarea.service';
import { Router } from '@angular/router';
import { UtilityService } from '@app/core/utility/utility.service';
import { Rate } from '@app/shared/interfaces/iRate';

@Component({
  selector: 'app-personalarea',
  templateUrl: './personalarea.component.html',
  styleUrls: ['./personalarea.component.scss']
})
export class PersonalareaComponent implements OnInit {

  constructor(
    private personalareaService: PersonalareaService,
    private router: Router,
    private utility: UtilityService,
  ) { }

  public _personalarea: Personalarea;
  public _tasks: Tasks[];
  public _team: Team[];
  public _rate: Rate = {Id: 0};

  ngOnInit() {

    this.personalareaService.getPersonalarea()
      .subscribe(
        personalarea => {
          this._personalarea = personalarea;
          console.log(personalarea);
        },
        err => { // If Unauth
          this.utility.handleHttpError(err);
          this.router.navigateByUrl('auth');
        },
        () => console.log('HTTP request completed.'));

    this.personalareaService.getTasks()
      .subscribe(
        tasks => {
          this._tasks = tasks.reverse();
          this._tasks = [...tasks];
          console.log(tasks);
        },
        err => { // If Unauth
          this.utility.handleHttpError(err);
          this.router.navigateByUrl('auth');
        },
        () => console.log('HTTP request completed.'));

    this.personalareaService.getTeam()
      .subscribe(
        team => {
          this._team = [...team];
          console.log(team);
        },
        err => { // If Unauth
          this.utility.handleHttpError(err);
          this.router.navigateByUrl('auth');
        },
        () => console.log('HTTP request completed.'));

    this.personalareaService.getRate()
      .subscribe(
        rate => {
          this._rate = {...rate};
          console.log(rate);
        },
        err => { // If Unauth
          this.utility.handleHttpError(err);
          this.router.navigateByUrl('auth');
        },
        () => console.log('HTTP request completed.'));
  }
  
  createPost(post: Tasks) {
    this._tasks = [post, ...this._tasks];
    }

}
