import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUserDTO } from '../user.interface';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css'],
})
export class AddUserComponent implements OnInit {
  public userModel= {} as IUserDTO;
  private _userSubscribtion : Subscription;
  
  constructor(private userService:UserService,private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {
  }

  close(){
    this.dialogRef.close();
  }

  onSubmit(userForm:NgForm){
    if(userForm.valid){
      this.userService.addUser(this.userModel).subscribe( {
        next: x => console.log("User Added"),
        error: err => {console.log("Error occured in User service"); console.error(err);}
      })
      this.dialogRef.close();
    }
  }

}
