import {createReducer,on,Action} from '@ngrx/store';
import { addUserAction } from './users.actions';

/**
 * User Reducer.
 */

 export const userReducer = createReducer(
    {},
    on(addUserAction,state=>{
        return{
            ...state,
            // something
        }
    })
)