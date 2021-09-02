import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IUser as IUserDTO } from './users/user.interface';
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
  getAllUsers() : Observable<IUserDTO[]> {
    return this.httpClient.get<IUserDTO[]>(this.api).pipe(
      catchError(this.handleError)
    );
  }

  getUserDetails(id:number):Observable<IUserDTO>{
    return this.httpClient.get<IUserDTO>(this.api+`?userId=${id}`).pipe(
      catchError(this.handleError)
    )
  }
  
  updateUser(id:number,user:IUserDTO){
    return this.httpClient.put(this.api,user).pipe(
      catchError(this.handleError)
    )
  }

  addUser(user:IUserDTO){
    return this.httpClient.post<IUserDTO>(this.api,user).pipe(
      catchError(this.handleError)
    )
  }
  
  deleteUser(id:number){
    return this.httpClient.delete(this.api+`?userId=${id}`).pipe(
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
