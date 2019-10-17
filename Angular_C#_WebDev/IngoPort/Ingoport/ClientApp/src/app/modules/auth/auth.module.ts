import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import {
  AuthpageComponent,
  AuthContentComponent,
  AuthFormComponent,
  RegisterFormComponent,
  RoleCheckboxComponent
} from '@modules/auth/components/';

import { SharedModule } from '@shared/shared.module';
@NgModule({
  declarations: [
    AuthpageComponent,
    AuthContentComponent,
    AuthFormComponent,
    RegisterFormComponent,
    RoleCheckboxComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule
  ],
})
export class AuthModule { }
