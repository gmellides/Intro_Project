import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  public inputUser: IUser;
  private _userSubscribtion : Subscription;
  private addUserFormControl = new FormControl();
  constructor(private userService:UserService,private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {

  }

  close(){
    this.dialogRef.close();
  }

  save(){

  }

}
