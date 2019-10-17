import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Components for Router
import { AppComponent } from './app.component';
import { AuthpageComponent } from '@modules/auth/components';
import { HomepageComponent } from '@modules/home/components';
import { BotComponent } from '@modules/bot/components';
import { InternsComponent } from '@modules/interns/components';
import { PersonalareaComponent } from '@modules/personalarea/components';
import { QnaPageComponent } from '@modules/q-and-a/components'
import { ErrorPageComponent } from '@modules/404page/components';


const routes: Routes = [
  { path: '', component: AuthpageComponent, data: { animation: 'home' } },
  { path: 'news', component:  HomepageComponent, data: { animation: 'home' }},
  { path: 'auth', component: AuthpageComponent, data: { animation: 'auth' } },
  { path: 'bot', component: BotComponent, data: { animation: 'bot' } },
  { path: 'interns', component: InternsComponent, data: { animation: 'interns' } },
  { path: 'personal_area', component: PersonalareaComponent , data: { animation: 'personal_area' }},
  { path: 'questions_and_answers', component:  QnaPageComponent},
  { path: '**', component: ErrorPageComponent, data: { animation: 'home' } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
