import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { switchMap } from "rxjs/operators";
import { UserService } from "../user.service";
import { getRegisteredUsers } from "./users.actions";

@Injectable()
export class UsersEffects{
    /**
     *
     */
    constructor(private actions$:Actions, private _userService:UserService) {
        
    }


    loadUsers$=createEffect(()=>
        this.actions$.pipe(
            ofType(getRegisteredUsers),
            switchMap( action =>
                this._userService.getAllUsers().pipe(map(x=> ))
        ))
    );
    
}