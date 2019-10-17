import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'


import { 
  InternsComponent,
  
 } from '@modules/interns/components/';
import { QuillModule } from 'ngx-quill';



@NgModule({
  declarations: [InternsComponent],
  imports: [
    CommonModule 
  ]
})
export class InternsModule { }
