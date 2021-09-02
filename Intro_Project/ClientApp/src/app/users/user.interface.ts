export interface IUserTitle{
    Id?:number;
    Description:string;
}

export interface IUserType{
    Id?:number;
    Description:string;
    Code:string;
}

export interface IUser{
    Id?:number;
    Name:string;
    Surname:string;
    BirthDate?:Date;
    UserType?:IUserType;
    UserTitle?:IUserTitle;
    EmailAddress:string;
}

export interface IUserDTO{
    Id?:number;
    Name:string;
    Surname:string;
    BirthDate?:Date;
    UserTypeDescription:string;
    UserTypeCode:string;
    UserTitleDescription:string;
    EmailAddress:string;
}
