import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { 
  RankComponent,
  UserListComponent,
 } from '@modules/rank/components/';
import { TopChartComponent } from './components/rank/top-chart/top-chart.component';
import { ShowAllUsersComponent } from './components/rank/show-all-users/show-all-users.component';






@NgModule({
  declarations: [
    RankComponent,
    UserListComponent,
    TopChartComponent,
    ShowAllUsersComponent,
  ],
  imports: [
    CommonModule
  ]
})
export class RankModule { }
