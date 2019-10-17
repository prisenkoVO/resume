import { Component, OnInit, Output, EventEmitter } from '@angular/core';


import { NewsService } from '@core/news/news.service';
import { UtilityService } from '@core/utility/utility.service';
import { FileService } from '@app/core/files/file.service';

import { News } from '@app/shared/interfaces/iNews';


@Component({
  selector: 'app-suggest-post',
  templateUrl: './suggest-post.component.html',
  styleUrls: ['./suggest-post.component.scss']
})
export class SuggestPostComponent implements OnInit {

  constructor(
    private newsService: NewsService,
    private utility: UtilityService,
    private files: FileService,
  ) { }

  @Output() newPost = new EventEmitter();

  public _showForm = false;

  public _showFormMessage = 'Предложить новость';

  public _postTitle: string;
  public _postText: string;

  private _fileText = '';
  private fileToUpload: any = null;
  public fileToPreview: any = null;

  ngOnInit() {
  }

  toggleShowForm() {
    this._showForm = !this._showForm;
    this._showFormMessage === 'Предложить новость' ? this._showFormMessage = 'Отмена' : this._showFormMessage = 'Предложить новость';
  }

  suggestPost(Title: string, Text: string) {

    // TODO: add fileds here
    const post: News = {
      Photo: 'Hello',
      Title,
      Text,
      UserPhoto: 'User Photo',
      Author: this.utility.getCookie('name'),
    };


    if (this.fileToUpload === null) {
      console.log(post);
      this.newsService.postNews(post)
      .subscribe(
        res => {
          // Also add fields here
          const article: News = {
            Title,
            Text,
            Photo: null,
            isLiked: false,
            Likes: 0,
            comments: [],
            UserPhoto: window.localStorage.getItem('photo'),
            Author: this.utility.getCookie('name'),
          };

          this.newPost.emit(article);
          this.toggleShowForm();
          this._postText = '';
          this._postTitle = null;
          this.fileToPreview = null;
        },
        err => this.utility.handleHttpError(err),
      );
    } else {

      const reader = new FileReader();

      reader.onloadend = e => {
        this.fileToUpload = reader.result;
        post.Photo = this.fileToUpload;
        this.newsService.postNews(post)
        .subscribe(
          res => {
            // TODO: add fields here
            const article: News = {
              
              Title,
              Text,
              Photo: this.fileToUpload,
              isLiked: false,
              Likes: 0,
              comments: [],
              UserPhoto: window.localStorage.getItem('photo'),
              Author: this.utility.getCookie('name'),
            };

            this.newPost.emit(article);
            this.toggleShowForm();
            this._postText = '';
            this._postTitle = null;
            this.fileToPreview = null;
          },
          err => this.utility.handleHttpError(err),
        );
      }
      reader.readAsDataURL(this.fileToUpload);
    }
  }


  readFile(event) {
      const myReader: FileReader = new FileReader();
      const file = event.target.files.item(0);
      this.fileToPreview = file;
      this.fileToUpload = file;
      myReader.onloadend = (e) => {

        this.fileToPreview = myReader.result;
        console.log(this.fileToPreview);
      }
      myReader.readAsDataURL(this.fileToPreview);
    }

    editorEvent(editorContent: any) {
      this._postText = editorContent.html;
    }
    deletePhoto(){
      this.fileToPreview = '';

    }
  }
