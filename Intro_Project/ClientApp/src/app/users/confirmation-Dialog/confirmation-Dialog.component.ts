import { Component, Input, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUserDTO } from '../user.interface';

@Component({
  selector: 'app-confirmation-Dialog',
  templateUrl: './confirmation-Dialog.component.html',
  styleUrls: ['./confirmation-Dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {

  @Input() public isEditUser:boolean = false;
  @Input() public isDeleteUser:boolean = false;
  @Input() public user:IUserDTO;
  public _userSubscription:Subscription;
  
  public modalTitle: string;
  public modalQuestion: string;
  
  constructor(private dialogRef: MatDialogRef<ConfirmationDialogComponent>,private router:Router,private userService:UserService) { }

  ngOnInit() {
    if(this.isEditUser){
      this.modalTitle = "Edit User Action - Confirmation Dialog";
      this.modalQuestion = "Are you sure that you want to edit user details?";
    }
    if(this.isDeleteUser){
      this.modalTitle = "Delete User Action - Confirmation Dialog";
      this.modalQuestion = "Are you sure that you want to Delete this User?";
    }
   
  }
  
  answerYesAction(){
    if(this.isEditUser){
      this._userSubscription = this.userService.updateUser(this.user).subscribe(
        {
          next: x =>console.log(x),
          error: err => console.log("Error occured in User service")
        }
      );
      this.dialogRef.close(); // Close dialog after request
      this.router.navigateByUrl('/users'); // navigate back to users page.
    }

    if(this.isDeleteUser){
      this._userSubscription = this.userService.deleteUser(this.user.id).subscribe(
        {
          next: x =>console.log(x),
          error: err => console.log("Error occured in User service")
        }
      );
      this.dialogRef.close(); // Dispoce Dialog After request
      this.router.navigateByUrl('/users'); //navigate back to users page.
    }
  }

  answerNoAction(){
    this.dialogRef.close();
  }

}
