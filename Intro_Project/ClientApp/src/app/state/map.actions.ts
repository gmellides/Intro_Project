import { createAction, props } from "@ngrx/store";

export const addPinAction = createAction('Add Pin',props<{ id,latitude,logitude }>());
export const removePinAction = createAction('Remove Pin',props<{ id }>());