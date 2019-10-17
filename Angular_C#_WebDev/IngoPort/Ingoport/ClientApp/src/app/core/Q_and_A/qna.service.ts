import { Injectable } from '@angular/core';
import { UtilityService } from '../utility/utility.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Topic } from '@app/shared/interfaces/iTopic';
import { Question } from '@app/shared/interfaces/iQuestion';

type FAQ = {
  Topics: Topic[],
  QuestionsAndAnswers: Question[],
};

@Injectable({
  providedIn: 'root'
})
export class QnaService {

  constructor(
    private utility: UtilityService,
    private http: HttpClient,
  ) { }

    private readonly HOST = this.utility.host;

  getQuestions(): Observable<FAQ> {
    return this.http.get<FAQ>(`${this.HOST}/api/qna`, {
      headers: this.utility.setHttpHeaders(),
    });
  }
}
