import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css'],
})
export class AddUserComponent implements OnInit {
  public userModel= {UserTitle:{},UserType:{}} as IUser;
  private _userSubscribtion : Subscription;
  public isInEditMode: boolean = false;
  
  constructor(private userService:UserService,private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {
  }

  close(){
    this.dialogRef.close();
  }

  onSubmit(user:NgForm){
    this.userService.addUser(this.userModel).subscribe( {
      next: x => console.log("User Added"),
      error: err => {console.log("Error occured in User service"); console.error(err);}
    })
  }

 

}
