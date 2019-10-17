import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-preloader',
  templateUrl: './preloader.component.html',
  styleUrls: ['./preloader.component.scss']
})
export class PreloaderComponent implements OnInit {

  @Input() showFullPreloader: boolean;
  @Input() showLittlePreloader: boolean;

  public fullPagePreloaderStyle = {
    background: 'var(--gray-bg)',
  }

  constructor() { }

  ngOnInit() {
  }

}
