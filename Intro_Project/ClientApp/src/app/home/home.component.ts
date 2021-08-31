import { OnInit } from '@angular/core';
import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { IUser } from '../users/user.interface';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { AddUserComponent } from '../users/add-user/add-user.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit,OnDestroy{
  public _usersCollection :IUser[];
  public _userSubscribtion:Subscription;

  constructor(private userService:UserService,private dialog:MatDialog){}

  mapLoadedEvent(status: boolean) {
    console.log('The map loaded: ' + status);
  }

  ngOnInit(): void {
    this._userSubscribtion = this.userService.getAllUsers().subscribe(
      {
        next: x => this._usersCollection = x,
        error: err => console.log("Error occured in User service")
      }
    );
    console.log(this._usersCollection);
  }

  openAddUserModal(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let addUserDialogRef =  this.dialog.open(AddUserComponent,matDialogConfig);
    addUserDialogRef.componentInstance.isInEditMode = false;
  }

  closeAddUserModal(){
    this.dialog.closeAll();
  }

  ngOnDestroy(): void {
    this._userSubscribtion.unsubscribe();
  }
}
