import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IUser } from './users/user.interface';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private api = " ";
  constructor(private httpClient:HttpClient) { }

  //#region API Methods
  getAllUsers() : Observable<IUser[]> {
    return this.httpClient.get<IUser[]>(this.api).pipe(
      catchError(this.handleError)
    );
  }
  
  updateUser(id:number,user:IUser){
  
  }

  addUser(user:IUser){

  }
  
  deleteUser(id:number){
    
  }
  //#endregion

  //#region Private Helpers
  private handleError(error :HttpErrorResponse){
    console.error(`HTTP Status: ${error.status} Message: ${error.message}`)
    return throwError(`HTTP Status: ${error.status} Message: ${error.message}`);
  }
  //#endregion
}
