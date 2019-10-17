import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { UtilityService } from '@core/utility/utility.service';

import { Personalarea } from '@app/shared/interfaces/iPersonalarea';
import { Tasks } from '@app/shared/interfaces/iTasks';
import { Team } from '@app/shared/interfaces/iTeam';
import { Rate } from '@app/shared/interfaces/iRate';

@Injectable({
  providedIn: 'root'
})
export class PersonalareaService {

  constructor(
    private http: HttpClient,
    private utility: UtilityService,
  ) { }

  private readonly HOST = this.utility.host;

  getPersonalarea(): Observable<Personalarea> {
    return this.http.get<Personalarea>(`${this.HOST}/api/personalarea/getInfo`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  getTasks(): Observable<Tasks[]> {
    return this.http.get<Tasks[]>(`${this.HOST}/api/personalarea/getTasks`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  getTeam(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.HOST}/api/personalarea/getTeam`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  getRate(): Observable<Rate> {
    return this.http.get<Rate>(`${this.HOST}/api/personalarea/rateme`, {
      headers: {
        'Authorization': this.utility.getAuthToken(),
        'Access-Control-Allow-Origin': '*',
      },
    });
  }


  postPersonalarea(post: Personalarea): Observable<Personalarea> {
    // Convert image to Base64
    const data = {
      Photo: post.Photo,
      Email: post.Email,
      Phone: post.Phone,
      Birth: post.Birth,
    }
    return this.http.put<Personalarea>(`${this.HOST}/api/settings`, data, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  postTasks(post: Tasks): Observable<Tasks> {

    const data = {
      Title: post.Title,
      Owner: post.Owner,
      TaskBody: post.TaskBody,
      InternId: post.InternId,
      Deadline: post.Deadline,
      FlagColor: post.FlagColor,
    }

    return this.http.post<Tasks>(`${this.HOST}/api/personalarea/new-task`, data, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  
  deleteTask(id: number): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/personalarea/delete-task/${id}`, {
      headers: this.utility.setHttpHeaders(),
    })
  }
}
