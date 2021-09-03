import { InputModalityDetector } from '@angular/cdk/a11y';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { AddUserComponent } from './add-user/add-user.component';
import {  IUserDTO } from './user.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users :IUserDTO[];
  public filteredUsers:IUserDTO[];

  public _userSubscribtion:Subscription;
  public _filterValue: string = '';
  displayedColumns = ["name", "Surname","BirthDate","email"];
  
  constructor(private userService:UserService,private router:Router,private dialog:MatDialog) { }

  ngOnInit() {
    this._userSubscribtion = this.userService.getAllUsers().subscribe(
      {
        next: x => {
          this.users = x;
          this.filteredUsers=this.users;
        },
        error: err => console.log("Error occured in User service")
      }
    );

  }
  
  openUserDetails(input:IUserDTO){
    if( Object.entries(input).length !== 0)
      this.router.navigateByUrl('/userDetails',{state:{userDetails:input}});
  }

  applyFiltering(criteria:string):IUserDTO[] {
    return this.users.filter((user: IUserDTO) =>
      user.name?.toLowerCase().includes(criteria.toLowerCase()) || user.surname?.toLowerCase().includes(criteria.toLowerCase()) );
  }


  // @Input()
  get filterValue():string{
    return this._filterValue;
  }
  set filterValue(value:string){
    this._filterValue = value;
    let results = this.applyFiltering(value);
    if(results!==undefined && results.length>0)
      this.filteredUsers = results;
    else
      this.filteredUsers = [{}] as IUserDTO[];
  }

  openAddUserDialog(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let addUserDialogRef =  this.dialog.open(AddUserComponent,matDialogConfig);
  }

}
