import { Component, Input, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUserDTO } from '../user.interface';
import {Router, ActivatedRoute, Params} from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-Dialog/confirmation-Dialog.component';
import { AddUserComponent } from '../add-user/add-user.component';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  templateUrl: './view-edit-user-details.component.html',
  styleUrls: ['./view-edit-user-details.component.css']
})
export class ViewEditUserDetailsComponent implements OnInit {
  
  public userDetails:IUserDTO;
  public usersBirthdate: Date;
  public isEditMode = false;
  public userId:number;
  
  constructor(private router:Router,private activatedRoute: ActivatedRoute,private dialog:MatDialog) { 
  }

  ngOnInit() {
    this.userDetails = history.state.userDetails; // get user details from history State.
  }
  
  // Open confirmation modal about updating User
  updateUser(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let editUserDialogRef =  this.dialog.open(ConfirmationDialogComponent,matDialogConfig);
    editUserDialogRef.componentInstance.isEditUser = true;
    editUserDialogRef.componentInstance.user = this.userDetails;
  }


  // Toogle from view to Edit mode.
  toogleEditMode(){
    this.isEditMode = !this.isEditMode
  }


  // Dispose Dialog.
  closeUserDetails(){
    this.router.navigateByUrl('/users');
  }

  // Open Delete user confirmation Dialog.
  deleteUser(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let deleteUserDialogRef =  this.dialog.open(ConfirmationDialogComponent,matDialogConfig);
    deleteUserDialogRef.componentInstance.isDeleteUser = true;
    deleteUserDialogRef.componentInstance.user = this.userDetails;
  }

}
