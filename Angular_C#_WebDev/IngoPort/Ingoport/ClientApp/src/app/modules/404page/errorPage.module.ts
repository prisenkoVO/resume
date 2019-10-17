import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorPageComponent } from './components';



@NgModule({
  declarations: [ErrorPageComponent],
  imports: [
    CommonModule
  ],
  exports:[ErrorPageComponent]
})
export class erorPageModule { }
