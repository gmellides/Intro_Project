import { createAction,props } from "@ngrx/store";

/**
 *  NgRx: User Actions 
 */
export const addUserAction = createAction('Add User: API',props<{ IUser }>());
export const removeUser = createAction('Remove User: API',props<{ userId }>());
export const editUser = createAction('Edit User: API',props<{ IUser }>());
export const retrieveUsers = createAction('Get All Users: API',props<{ IUser }>());