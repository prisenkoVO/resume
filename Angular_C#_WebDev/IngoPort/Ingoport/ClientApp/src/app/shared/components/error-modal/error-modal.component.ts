import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  styleUrls: ['./error-modal.component.scss']
})
export class ErrorModalComponent implements OnInit {

  constructor() { }

  @Input() error: string;
  @Input() code: number;

  @Output() closeModal = new EventEmitter();

  ngOnInit() {
    window.addEventListener('click', this.getElement);
  }

  ngOnDestroy() {
    window.removeEventListener('click', this.getElement);
  }

  getElement(evt) {
    console.log(evt.target)
  }

  onCloseModal() {
    window.removeEventListener('click', this.getElement);
    this.closeModal.emit();
  }

}
