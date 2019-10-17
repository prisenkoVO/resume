import { Component, OnInit, Input } from '@angular/core';
import { UtilityService } from '@app/core/utility/utility.service';
import { Personalarea } from '@app/shared/interfaces/iPersonalarea';

import { PersonalareaService } from '@core/personalarea/personalarea.service';
import { PersonalareaComponent } from '../personalarea.component';
@Component({
  selector: 'app-user-contacts',
  templateUrl: './user-contacts.component.html',
  styleUrls: ['./user-contacts.component.scss']
})
export class UserContactsComponent implements OnInit {

  constructor(
    private personalAreaService: PersonalareaService,
    private utility: UtilityService,
    ) { }

    public _showInput = true;
    public classOfButton = 'far fa-edit';

    private emailValue: string;
    private phoneValue: string;
    private dateBirthValue: string;

    private fileToPreview: any = null;

    public currentDate: string;

  @Input() Photo: any = null;
  @Input() Email:string;
  @Input() Phone:string;
  @Input() Birth:string;
 
  ngOnInit() {
    this.currentDate = this.utility.getCurrentDate().substring(0, 8).replace(/\./g, '-');
  }

  toggleShowInput() {
    this._showInput = !this._showInput;
    this.classOfButton === 'far fa-edit' ? this.classOfButton = 'far fa-save' : this.classOfButton = 'far fa-edit';

  }

  suggestInfo(Photo: string, Email: string, Phone: string, Birth:string) {
  
    const post: Personalarea = {
        Photo,
        Email,
        Phone,
        Birth,
    }  

    this.personalAreaService.postPersonalarea(post)
      .subscribe(
        res => {
          const post: Personalarea = {
            Photo,
            Email,
            Phone,
            Birth,
          };
        },
        err => this.utility.handleHttpError(err),
      );
    // Написать сервис и post запрос
  }
  readFile(event) {
    var myReader:FileReader = new FileReader();
    const file = event.target.files.item(0);
    this.Photo = file;
    myReader.onloadend = (e) => {

      this.Photo = myReader.result;
      console.log(this.Photo);
    }
    myReader.readAsDataURL(this.Photo);
  }
}
