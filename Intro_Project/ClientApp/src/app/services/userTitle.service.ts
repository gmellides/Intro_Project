import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, take } from 'rxjs/operators';
import { IUserTitleDTO } from '../users/user.interface';

@Injectable({
  providedIn: 'root'
})
export class UserTitleService {
  private api = "/api/UserTitles";

  constructor(private httpClient:HttpClient,@Inject('WEB_API_URL') webApiUrl: string) { 
    this.api = webApiUrl + this.api;
  }
  
  getAllUserTitles() : Observable<IUserTitleDTO[]> {
    return this.httpClient.get<IUserTitleDTO[]>(this.api).pipe(
      catchError(this.handleError)
    );
  }
  //#region Private Helpers
  private handleError(error :HttpErrorResponse){
    console.error(`HTTP Status: ${error.status} Message: ${error.message}`)
    return throwError(`HTTP Status: ${error.status} Message: ${error.message}`);
  }
  //#endregion
}
