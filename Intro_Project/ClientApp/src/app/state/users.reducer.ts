import {createReducer,on,Action} from '@ngrx/store';
import { IUser } from '../users/user.interface';
import { addUserAction, getRegisteredUsers } from './users.actions';

/**
 * User Reducer.
 */
export interface UserState{
    users:IUser[],
    isLoading:boolean
}

export const usersInitialState :UserState = {
    users:[],
    isLoading:false
}

export const userReducer = createReducer(
    usersInitialState,
    on(getRegisteredUsers, (state,action)=>{
        console.log("assaas");
        return{
            ...state,
            action
        }
    }),
    on(addUserAction,(state=>{
        // api call 
        return {
            ...state
        }
    }))
);