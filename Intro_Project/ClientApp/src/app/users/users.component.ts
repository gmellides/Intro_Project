import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { AddUserComponent } from './add-user/add-user.component';
import { IUser } from './user.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  public users :IUser[];
  public _userSubscribtion:Subscription;
  
  constructor(private userService:UserService,private dialog:MatDialog) { }

  ngOnInit() {
    this._userSubscribtion = this.userService.getAllUsers().subscribe(
      {
        next: x => this.users = x,
        error: err => console.log("Error occured in User service")
      }
    );

  }

  openAddUserDialog(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let addUserDialogRef =  this.dialog.open(AddUserComponent,matDialogConfig);
    addUserDialogRef.componentInstance.isInEditMode = false;
  }

}
