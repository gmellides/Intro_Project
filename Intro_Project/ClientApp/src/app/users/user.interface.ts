interface IUserTitle{
    Description:string;
}

interface IUserType{
    Description:string;
    Code:string;
}

export interface IUser{
    Id:number;
    Name:string;
    Surname:string;
    BirthDate?:Date;
    UserType:IUserType;
    UserTitle:IUserTitle;
    EmailAddress:string;
}