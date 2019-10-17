import { Component, OnInit, Input } from '@angular/core';
import { News } from '@app/shared/interfaces/iNews';
import { NewsService } from '@app/core/news/news.service';
import { UtilityService } from '@app/core/utility/utility.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {

  @Input() news: News[];

  constructor(
    private newsService: NewsService,
    private utility: UtilityService,
  ) { }

  ngOnInit() {}

  deletePost(id: number) {
    this.newsService.deletePost(id)
      .subscribe(
        res => {
          this.news = this.news.filter(post => post.Id !== id);
          
        },
        err => this.utility.handleHttpError(err),
      );
  }
}
