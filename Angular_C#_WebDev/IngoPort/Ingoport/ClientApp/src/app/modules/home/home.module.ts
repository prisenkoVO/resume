import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill';

import {
  HomepageComponent,
  SuggestPostComponent,
  NewsComponent
} from '@modules/home/components';



@NgModule({
  declarations: [
    HomepageComponent,
    SuggestPostComponent,
    NewsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    QuillModule.forRoot({
      modules: {
        toolbar: [
          ['bold', 'italic', 'underline', 'strike'],
          ['blockquote'],
          [{ 'header': 1 }, { 'header': 2 }],
          [{ 'list': 'ordered'}, { 'list': 'bullet' }],
          [{ 'align': [] }],
        ]
      },
      placeholder: 'Введите текст здесь!',
      format: 'text',
    }),
  ]
})
export class HomeModule { }
