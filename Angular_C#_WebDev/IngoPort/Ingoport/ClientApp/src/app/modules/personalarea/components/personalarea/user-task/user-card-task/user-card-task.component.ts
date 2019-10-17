import { Component, OnInit, Input } from '@angular/core';
import { PersonalareaService } from '@core/personalarea/personalarea.service';
import { UtilityService } from '@app/core/utility/utility.service';
import { Tasks } from '@app/shared/interfaces/iTasks';

@Component({
  selector: 'app-user-card-task',
  templateUrl: './user-card-task.component.html',
  styleUrls: ['./user-card-task.component.scss']
})
export class UserCardTaskComponent implements OnInit {
  public booleanVar: boolean = true;

  constructor(
    private personalAreaService: PersonalareaService,
    private utility: UtilityService,) { }

  @Input() Title:string;
  @Input() Author:string;
  @Input() Text:string;
  @Input() dateTime:string;
  @Input() deadLine:string;
  @Input() Flag:string;
  @Input() Owner:string;
  @Input() Id:number;
  @Input() tasks: Tasks[];
  
  
  
  ngOnInit() {
  }


  deleteTask(id: number) {
    this.booleanVar = !this.booleanVar;
    this.personalAreaService.deleteTask(id)
      .subscribe(
        res => {
          this.tasks
        },
        err => this.utility.handleHttpError(err),
      );
  }

}
