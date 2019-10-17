// TODO: BUG: when add 2 posts and more there adding more posts

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { UtilityService } from '@core/utility/utility.service';

import { News } from '@app/shared/interfaces/iNews';
import { PostComment } from '@shared/interfaces/iPostComment';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(
    private http: HttpClient,
    private utility: UtilityService,
  ) { }

  private readonly HOST = this.utility.host;

  getNews(): Observable<News[]> {
    return this.http.get<News[]>(`${this.HOST}/api/news`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  postNews(post: News): Observable<News> {
    // Convert image to Base64
    const data = {
      Title: post.Title,
      Text: post.Text,
      Photo: post.Photo,
      UserPhoto: post.UserPhoto,
    }
    return this.http.post<News>(`${this.HOST}/api/news`, data, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  toggleLike(Id: number): Observable<number> {
    return this.http.get<number>(`${this.HOST}/api/news/${Id}/like`, {
     headers: this.utility.setHttpHeaders(),
    });
  }

  addNewComment(comment: PostComment): Observable<PostComment> {
    console.log(comment);
    return this.http.post<PostComment>(`${this.HOST}/api/news/${comment.Id}/Comment`, comment, {
      headers: this.utility.setHttpHeaders()
    });
  }

  deletePost(id: number): Observable<any> {
    return this.http.delete<any>(`${this.HOST}/api/news/?id=${id}`, {
      headers: this.utility.setHttpHeaders(),
    })
  }
}
