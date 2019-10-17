import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@app/shared/shared.module';
import { FormsModule } from '@angular/forms';


import {
  PersonalareaComponent,
  UserCardTaskComponent,
  UserContactsComponent,
  UserTaskComponent,
  UserInfoComponent,
  UserProgressComponent,
  UserTeamComponent,
  ManageButtonComponent,
  TeammateCardComponent,
  UserAchievmentComponent,
  UserAchievmentItemComponent,
} from '@modules/personalarea/components/';
import { UserPhotoComponent } from './components/personalarea/user-photo/user-photo.component';









@NgModule({
  declarations: [
    PersonalareaComponent,
     UserContactsComponent,
     UserTaskComponent,
     UserCardTaskComponent,
     UserInfoComponent,
     UserProgressComponent,
      UserTeamComponent,
      ManageButtonComponent,
      TeammateCardComponent,
      UserAchievmentComponent,
      UserAchievmentItemComponent,
      UserPhotoComponent,
    ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
  ]
})
export class PersonalareaModule { }
