import { Component, OnInit } from '@angular/core';
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
  public userModel= {} as IUser;
  private _userSubscribtion : Subscription;
  public isInEditMode: boolean = false;
  
  constructor(private userService:UserService,private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {
  }

  close(){
    this.dialogRef.close();
  }

  save(){
    console.log(this.userModel)
  }

  onSubmit(){
    console.log("asdasdasdasd")
  }

}
