import { AfterViewInit, Component, OnInit } from "@angular/core";
import {
  FormControl,
  FormGroup, Validators
} from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
import { take } from "rxjs/operators";
import { UserService } from "src/app/services/user.service";
import { UserTitleService } from "src/app/services/userTitle.service";
import { UserTypeService } from "src/app/services/userType.service";
import { ICreateOrEditUserDTO, IUserTitleDTO, IUserTypeDTO } from "../user.interface";

@Component({
  selector: "app-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.css"],
})
export class AddUserComponent implements OnInit {
  public createUpdateUserDTO = {} as ICreateOrEditUserDTO;
  public userId :number;
  public editUserMode = false;
  public userFormGroup: FormGroup;
  public userTitles: IUserTitleDTO[];
  public userTypes: IUserTypeDTO[];

  constructor(
    private userService: UserService,
    private userTitleService: UserTitleService,
    private userTypeService: UserTypeService,
    private dialogRef: MatDialogRef<AddUserComponent>
  ) {}

  ngOnInit() {
    debugger;
    
    //#region Get Dropdown options
    if(this.userTitles == undefined){
      this.userTitleService
      .getAllUserTitles()
      .pipe(take(1))
      .subscribe({
        next: (x) => (this.userTitles = x),
        error: (err) => {
          console.log("Error occured in User service");
          console.error(err);
        },
      });
    }
    if(this.userTypes == undefined){
      this.userTypeService
      .getAllUserTypes()
      .pipe(take(1))
      .subscribe({
        next: (x) => (this.userTypes = x),
        error: (err) => {
          console.log("Error occured in User service");
          console.error(err);
        },
      });
    }
    //#endregion
    
    //#region Initialize formgroup validatiors
    this.userFormGroup = new FormGroup({
      name: new FormControl("", Validators.compose([Validators.maxLength(20)])),
      surname: new FormControl(
        "",
        Validators.compose([Validators.maxLength(20)])
      ),
      mailAddress: new FormControl("", Validators.compose([Validators.email])),
      birthdate:new FormControl(""),
      typeDescription:new FormControl(""),
      typeCode:new FormControl(""),
      titleDescription:new FormControl("")
    });
    //#endregion
  
  }

  ngAfterViewInit(){
    
  

    //#region Load user values in edit user form
    if (this.editUserMode) {
      this.userFormGroup.get("name").setValue(this.createUpdateUserDTO.name);
      this.userFormGroup.get("surname").setValue(this.createUpdateUserDTO.surname);
      this.userFormGroup
        .get("mailAddress")
        .setValue(this.createUpdateUserDTO.emailAddress);
        console.log(this.createUpdateUserDTO.birthDate);
      this.userFormGroup.get("birthdate").setValue(this.createUpdateUserDTO.birthDate);
      let selectedType = this.userTypes.filter(x=>x.id==this.createUpdateUserDTO.userTypeId)[0];
      let selectedTitle = this.userTitles.filter(x=>x.id == this.createUpdateUserDTO.userTitleId)[0];
      this.userFormGroup
        .get("titleDescription")
        .setValue(selectedTitle);
      this.userFormGroup.get("typeCode").setValue(selectedType);
      this.userFormGroup
      .get("typeDescription")
      .setValue(selectedType);
    }
    //#endregion
  }

  // Select list onChange Events
  onTypeCodeChange(){
    let selectedCode = this.userFormGroup.get("typeCode").value;
    this.userFormGroup.get("typeDescription").setValue(selectedCode);
  }

  onTypeDescriptionChange(value:string){
    let selectedType = this.userFormGroup.get("typeDescription").value;
    this.userFormGroup
        .get("typeCode")
        .setValue(selectedType);
  }

  close() {
    this.dialogRef.close();
  }

  onSubmit() {
    this.userId=this.createUpdateUserDTO.id;
    let selectedType:IUserTypeDTO = this.userFormGroup.get("typeCode").value;
    let selectedTitle:IUserTitleDTO = this.userFormGroup.get("titleDescription").value;
    this.createUpdateUserDTO = {
      name: this.userFormGroup.get("name").value,
      surname: this.userFormGroup.get("surname").value,
      birthDate: this.userFormGroup.get("birthdate")?.value || null,
      emailAddress: this.userFormGroup.get("mailAddress").value,
      userTitleId: selectedTitle.id,
      userTypeId: selectedType.id,
    };

    if (this.editUserMode) {
      console.log(this.createUpdateUserDTO);
      this.userService
        .updateUser(this.userId, this.createUpdateUserDTO)
        .pipe(take(1))
        .subscribe({
          next: (x) => console.log(x),
          error: (err) => console.log("Error occured in User service"),
        });
    } else {
      this.userService
        .addUser(this.createUpdateUserDTO)
        .pipe(take(1))
        .subscribe({
          next: (x) => console.log("User Added"),
          error: (err) => {
            console.log("Error occured in User service");
            console.error(err);
          },
        });
    }

    this.dialogRef.close();
  }
}
