export interface IUserDTO{
    id?:number;
    name:string;
    surname:string;
    birthDate?:Date;
    userTypeDescription:string;
    userTypeCode:string;
    userTitleDescription:string;
    emailAddress:string;
}

export interface ICreateOrEditUserDTO{
    id?:number;
    name:string;
    surname:string;
    birthDate?:Date;
    userTypeId:number;
    userTitleId:number;
    emailAddress:string;
}

export interface IUserTitleDTO{
    id?:number;
    description:string;
}

export interface IUserTypeDTO{
    id?:number;
    description:string;
    code:string;
}

export interface ISearchUserDTO{
    fullName?:string;
}