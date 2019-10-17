import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UtilityService } from '@core/utility/utility.service';
import { NewsService } from '@core/news/news.service';
import { News } from '@app/shared/interfaces/iNews';
import { Router } from '@angular/router';



@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent implements OnInit {

  constructor(
    private newsService: NewsService,
    private router: Router,
    private utility: UtilityService,
  ) { }

  public _showNewsPreloader = true;

  public _news: News[];

  ngOnInit() {
    this.newsService.getNews()
      .subscribe(
        news => { // Get news and toggle Preloader
          this._news = news.reverse();
          this._showNewsPreloader = false;
        },
        err => { // If Unauth
          this.utility.handleHttpError(err);
          this.router.navigateByUrl('auth');
        },
        () => console.log('HTTP request completed.'))
  }

  createPost(post: News) {
    this._news = [post, ...this._news];
  }
}
