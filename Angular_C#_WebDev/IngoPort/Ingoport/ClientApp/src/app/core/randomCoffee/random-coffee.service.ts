import { Injectable } from '@angular/core';
import { UtilityService } from '../utility/utility.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { FeedBack } from '@shared/interfaces/iFeedBack';


// TODO: replace all any obs to data structures
@Injectable({
  providedIn: 'root'
})
export class RandomCoffeeService {

  constructor(
    private utility: UtilityService,
    private http: HttpClient,
  ) { }

  private readonly HOST = this.utility.host;
  
  public getCoffeeStatus(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  // Starts new cycle of random coffee
  public enrollRandomCoffee(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee/enroll`, {
      headers: this.utility.setHttpHeaders(),
    });
  }


  // Cancel current random coffee cycle
  public refuseRandomCoffee(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee/refuse`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  // Get all stats from random coffee
  public getCoffeeStats(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee/statistics`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  // Get random coffee history of user
  public getCoffeeHistory(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee/history`, {
      headers: this.utility.setHttpHeaders(),
    })
  }

  // Get pair of user
  public getCoffeePair(): Observable<any> {
    return this.http.get<any>(`${this.HOST}/api/coffee/mypair`, {
      headers: this.utility.setHttpHeaders(),
    });
  }

  public leaveFeedback(feedBack: FeedBack): Observable<any> {
    return this.http.post<any>(`${this.HOST}/api/coffee`, {
      headers: this.utility.setHttpHeaders(),
    });
  }
}
