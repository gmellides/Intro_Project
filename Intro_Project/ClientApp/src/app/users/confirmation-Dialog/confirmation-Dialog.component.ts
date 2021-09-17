import { Component, Input, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';
import { IUserDTO } from '../user.interface';

@Component({
  selector: 'app-confirmation-Dialog',
  templateUrl: './confirmation-Dialog.component.html',
  styleUrls: ['./confirmation-Dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {
  @Input() public isDeleteUser:boolean = false;
  @Input() public user: IUserDTO;

  
  public modalTitle: string;
  public modalQuestion: string;
  
  constructor(private dialogRef: MatDialogRef<ConfirmationDialogComponent>,private router:Router,private userService:UserService) { }

  ngOnInit() {
    if(this.isDeleteUser){
      this.modalTitle = "Delete User Action - Confirmation Dialog";
      this.modalQuestion = "Are you sure that you want to Delete this User?";
    }
   
  }
  
  answerYesAction(){

    if(this.isDeleteUser){
      this.userService.deleteUser(this.user.id).subscribe(
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
