import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { NewsService } from '@app/core/news/news.service';
import { UtilityService } from '@app/core/utility/utility.service';

import { News } from '@shared/interfaces/iNews';
import { PostComment } from '@shared/interfaces/iPostComment';

// TODO: IF comments === 0 then dont

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {

  @Input() post: News;
  @Output() deletePost = new EventEmitter();

  constructor(
    private news: NewsService,
    private utility: UtilityService,
  ) { }

  public likeClass = 'far';

  public showComments = false;
  public showCommentsText = 'Прочитать комментарии';

  public isUsersPost = false;

  ngOnInit() {
    if (this.post.isLiked) {
      this.likeClass = 'fas';
    } else {
      this.likeClass = 'far';
    }

    const name = this.utility.getCookie('name');
    name === this.post.Author ? this.isUsersPost = true : this.isUsersPost = false;
  }

  toggleLike() {
    this.news.toggleLike(this.post.Id)
    .subscribe(
      id => {
        this.post.isLiked = !this.post.isLiked;

        if (this.post.isLiked) {
          this.likeClass = 'fas';
          this.post.Likes = ++this.post.Likes;
        } else {
          this.likeClass = 'far';
          this.post.Likes = --this.post.Likes;
        }
      }
    );
  }

  toggleComments() {
    this.showComments = !this.showComments;
    this.showCommentsText === 'Прочитать комментарии'?
      this.showCommentsText = 'Скрыть комментарии': this.showCommentsText = 'Прочитать комментарии'
  }

  writeComment(text: string) {
    const comment: PostComment = {
      Id: this.post.Id,
      commentText: text,
    };

    this.news.addNewComment(comment)
      .subscribe(
        res => {
          console.log(res);
          this.toggleComments();

          this.post.comments = [{ Id: comment.Id, Text: comment.commentText }, ...this.post.comments];
        },
        err => this.utility.handleHttpError(err),
      );
  }

  onDeletePost() {
    this.deletePost.emit(this.post.Id);
  }
}
