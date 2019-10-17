import { Injectable } from '@angular/core';


import { UtilityService } from '@core/utility/utility.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(
    private utility: UtilityService,
    private http: HttpClient,
  ) { }

  private readonly HOST = this.utility.host;

  sendFile(file: File, postId: number): Promise<any> {
    const formData = new FormData();

    formData.append('file', file, file.name);
    formData.append('id', `${postId}`);

    // api/news/{id}/image
    return this.http.post(`${this.HOST}/api/image/post`, file, {
      headers: {
        'Content-type': file.type,
        'Authorization': this.utility.getAuthToken(),
        'Access-Control-Allow-Origin': '*',
      }
    }).toPromise();
  }
}
