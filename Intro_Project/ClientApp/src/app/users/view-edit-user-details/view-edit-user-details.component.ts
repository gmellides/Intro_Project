import { Component, OnInit } from "@angular/core";
import {
  ICreateOrEditUserDTO,
  IUserDTO,
  IUserTitleDTO,
  IUserTypeDTO,
} from "../user.interface";
import { Router } from "@angular/router";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { ConfirmationDialogComponent } from "../confirmation-Dialog/confirmation-Dialog.component";
import { AddUserComponent } from "../add-user/add-user.component";
import { UserTitleService } from "src/app/services/userTitle.service";
import { UserTypeService } from "src/app/services/userType.service";
import { take } from "rxjs/operators";

@Component({
  selector: "app-user-details",
  templateUrl: "./view-edit-user-details.component.html",
  styleUrls: ["./view-edit-user-details.component.css"],
})
export class ViewEditUserDetailsComponent implements OnInit {
  public userDetails: IUserDTO;
  public createEditUserDTO: ICreateOrEditUserDTO;
  public usersBirthdate: Date;
  public isEditMode = false;
  public userId: number;
  public userTitles: IUserTitleDTO[];
  public userTypes: IUserTypeDTO[];

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private userTitleService: UserTitleService,
    private userTypeService: UserTypeService
  ) {}

  ngOnInit() {
    this.userDetails = history.state.userDetails; // get user details from history State.

    //#region Get Dropdown options
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
    console.log(this.userTitles);
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
    //#endregion
  }

  // Open confirmation modal about updating User
  updateUser() {
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let editUserDialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      matDialogConfig
    );
    editUserDialogRef.componentInstance.user = this.userDetails;
  }

  // Toogle from view to Edit mode.
  toogleEditMode() {
    let title = this.userTitles.filter(
      (x) => x.description == this.userDetails.userTitleDescription
    )[0];
    let type = this.userTypes.filter(
      (x) => x.code == this.userDetails.userTypeCode
    )[0];
    this.createEditUserDTO = {
      id: this.userDetails.id,
      name: this.userDetails.name,
      surname: this.userDetails.surname,
      emailAddress: this.userDetails.emailAddress,
      birthDate: this.userDetails.birthDate,
      userTitleId: title.id,
      userTypeId: type.id,
    };

    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let editUserDialogRef = this.dialog.open(AddUserComponent, matDialogConfig);
    editUserDialogRef.componentInstance.createUpdateUserDTO = this.createEditUserDTO;
    editUserDialogRef.componentInstance.editUserMode = true;
    editUserDialogRef.componentInstance.userTypes = this.userTypes;
    editUserDialogRef.componentInstance.userTitles = this.userTitles;
  }

  // Dispose Dialog.
  closeUserDetails() {
    this.router.navigateByUrl("/users");
  }

  // Open Delete user confirmation Dialog.
  deleteUser() {
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let deleteUserDialogRef = this.dialog.open(
      ConfirmationDialogComponent,
      matDialogConfig
    );
    deleteUserDialogRef.componentInstance.isDeleteUser = true;
    deleteUserDialogRef.componentInstance.user = this.userDetails;
  }
}
