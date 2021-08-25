import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IUser } from './users/user.interface';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private api = "/api/Users";
  
  constructor(private httpClient:HttpClient,@Inject('WEB_API_URL') webApiUrl: string) { 
    this.api = webApiUrl + this.api;
    console.log(`web api: ${this.api}`);
  }

  //#region API Methods   
  getAllUsers() : Observable<IUser[]> {
    return this.httpClient.get<IUser[]>(this.api).pipe(
      catchError(this.handleError)
    );
  }
  
  updateUser(id:number,user:IUser){
    return this.httpClient.put(this.api,user).pipe(
      catchError(this.handleError)
    )
  }

  addUser(user:IUser){
    return this.httpClient.post(this.api,user).pipe(
      catchError(this.handleError)
    )
  }
  
  deleteUser(id:number){
    return this.httpClient.delete(this.api).pipe(
      catchError(this.handleError)
    )
  }
  //#endregion

  //#region Private Helpers
  private handleError(error :HttpErrorResponse){
    console.error(`HTTP Status: ${error.status} Message: ${error.message}`)
    return throwError(`HTTP Status: ${error.status} Message: ${error.message}`);
  }
  //#endregion
}
