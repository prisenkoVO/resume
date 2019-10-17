import { Component, OnInit } from '@angular/core';
import { QnaService } from '@app/core/Q_and_A/qna.service';
import { UtilityService } from '@app/core/utility/utility.service';
import { Topic } from '@app/shared/interfaces/iTopic';
import { Question } from '@app/shared/interfaces/iQuestion';

type QNAQuestion = {
  Question: string;
  Answer: string;
}

@Component({
  selector: 'app-qna-page',
  templateUrl: './qna-page.component.html',
  styleUrls: ['./qna-page.component.scss']
})
export class QnaPageComponent implements OnInit {

  constructor(
    private questions: QnaService,
    private utility: UtilityService,
  ) { }

  public Topics: Topic[];
  public Questions: Question[];

  public showPreloader = true;
  public showQuestions = false;

  public question: QNAQuestion;


  ngOnInit() {
    this.loadQuestions();
  }

  loadQuestions() {
    this.questions.getQuestions()
      .subscribe(
        res => {
          this.showPreloader = false;
          console.log(res);
          this.Topics = [...res.Topics];
          this.Questions = [...res.QuestionsAndAnswers];
        },
        err => this.utility.handleHttpError(err),
      );
  }

  changeQuestions(id: number) {
    this.showQuestions = true;

    this.Questions.forEach((element, index) => {
      if (element.TopicId === id) {
        this.question = {
          Question: element.Question,
          Answer: element.Answer[0].Text,
        }
      }
    })

  }
}
