import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Tasks } from '@app/shared/interfaces/iTasks';
import { Team } from '@app/shared/interfaces/iTeam';
import { Personalarea } from '@app/shared/interfaces/iPersonalarea';

import { PersonalareaService } from '@core/personalarea/personalarea.service';
import { TeammateCardComponent } from '../user-team/teammate-card/teammate-card.component';
import { UtilityService } from '@app/core/utility/utility.service';

@Component({
  selector: 'app-user-task',
  templateUrl: './user-task.component.html',
  styleUrls: ['./user-task.component.scss']
})
export class UserTaskComponent implements OnInit {

  constructor(
    private personalAreaService: PersonalareaService,
    private utility: UtilityService,
    ) { }

  


  @Input() FirstName: string;
  @Input() LastName: string;
  @Input() Id: number;
  @Input() RoleId: number;
  @Input() Photo: string;
  @Input() Tasks: Tasks[];
  @Input() Team: Team[];
  
  @Output() newTask = new EventEmitter();

  // selectedIntern = 'Не выбран';

  ngOnInit() {
  }

  suggestTask(Title: string, Owner: string, TaskBody: string, InternId: number, Deadline: string, FlagColor: string) {
  
    const post: Tasks = {
      Title,
      Owner,
      TaskBody,
      InternId,
      Deadline,
      FlagColor,
    };  
    console.log(post);
    this.personalAreaService.postTasks(post)
      .subscribe(
        res => {
          const post: Tasks = {
            Title,
            Owner,
            TaskBody,
            InternId,
            Deadline,
            FlagColor,
          };
          
          this.newTask.emit(post);
        },
        err => this.utility.handleHttpError(err),
      );
  }

}
