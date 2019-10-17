import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-to-top-button',
  templateUrl: './to-top-button.component.html',
  styleUrls: ['./to-top-button.component.scss']
})
export class ToTopButtonComponent implements OnInit {

  constructor() { }

  public showButton = false;

  ngOnInit() {
    window.addEventListener('scroll', evt => {
      if (scrollY >= window.innerHeight) {
        this.showButton = true;
      } else {
        this.showButton = false;
      }
    });
  }

  scrollToTop(): void {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  }
}
