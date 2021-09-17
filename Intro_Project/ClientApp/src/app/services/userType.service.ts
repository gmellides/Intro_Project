import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IUserTypeDTO } from '../users/user.interface';

@Injectable({
  providedIn: 'root'
})
export class UserTypeService {

  private api = "/api/UserTypes";

  constructor(private httpClient:HttpClient,@Inject('WEB_API_URL') webApiUrl: string) { 
    this.api = webApiUrl + this.api;
  }
  
  getAllUserTypes() : Observable<IUserTypeDTO[]> {
    return this.httpClient.get<IUserTypeDTO[]>(this.api).pipe(
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
