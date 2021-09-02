import { createAction,props } from "@ngrx/store";
import { IUser } from "../users/user.interface";

/**
 *  NgRx: User Actions 
 */
export const getRegisteredUsers = createAction('Set All Users',props<{ users:IUser[] }>());

export const addUserAction = createAction('Add User: API',props<{ IUser }>());
export const removeUser = createAction('Remove User: API',props<{ userId }>());
export const editUser = createAction('Edit User: API',props<{ IUser }>());