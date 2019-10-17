import { Component, OnInit, Input } from '@angular/core';
import { Rate } from '@app/shared/interfaces/iRate';

@Component({
  selector: 'app-user-progress',
  templateUrl: './user-progress.component.html',
  styleUrls: ['./user-progress.component.scss']
})
export class UserProgressComponent implements OnInit {

  constructor() { }

  @Input() Likes: number;
  @Input() Comment: number;

  ngOnInit() {
  }

}
