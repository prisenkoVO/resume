import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

import { PostComment } from '@app/shared/interfaces/iPostComment';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {

  constructor() { }

  @Input() Comments: PostComment[];

  @Output() writeComment = new EventEmitter();

  public commentText: string;

  ngOnInit() {
    // console.log(this.Comments.length);
    console.log(this.Comments);
  }


  onWriteComment() {
    if(this.commentText === null || this.commentText.replace(/' '/g, '').length >= 1) {
      this.writeComment.emit(this.commentText);
    }
  }
}
