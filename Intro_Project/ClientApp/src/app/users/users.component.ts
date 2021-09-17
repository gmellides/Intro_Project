import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
import { UserService } from '../services/user.service';
import { AddUserComponent } from './add-user/add-user.component';
import { ISearchUserDTO, IUserDTO } from './user.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public users :IUserDTO[];
  public filteredUsers:IUserDTO[];
  public searchUserValue: ISearchUserDTO = {};
  public _filterValue: string = '';
  displayedColumns = ["name", "Surname","BirthDate","email"];
  
  constructor(private userService:UserService,private router:Router,private dialog:MatDialog) { }

  ngOnInit() {
    this.userService.getAllUsers().pipe(take(1)).subscribe(
      {
        next: x => {
          this.users = x;
          this.filteredUsers=this.users;
        },
        error: err => console.log("Error occured in User service")
      }
    );

  }

  searchUsers(){
    this.searchUserValue.fullName = this._filterValue; 
    this.userService.filterUsers(this.searchUserValue).pipe(take(1)).subscribe({
      next: x=>{this.filteredUsers=x},
      error: err=>console.log("Error occured with filter users")
    });
  }
  
  openUserDetails(input:IUserDTO){
    if(Object.entries(input).length !== 0)
      this.router.navigateByUrl('/userDetails',{state:{userDetails:input}});
  }

  applyFiltering(criteria:string):IUserDTO[] {
    return this.users.filter((user: IUserDTO) =>
      user.name?.toLowerCase().includes(criteria.toLowerCase()) || user.surname?.toLowerCase().includes(criteria.toLowerCase()) );
  }

  get filterValue():string{
    return this._filterValue;
  }
  set filterValue(value:string){
    this._filterValue = value;
    this.filteredUsers = this.users;
  }

  openAddUserDialog(){
    const matDialogConfig = new MatDialogConfig();
    matDialogConfig.disableClose = true;
    matDialogConfig.autoFocus = true;

    let addUserDialogRef =  this.dialog.open(AddUserComponent,matDialogConfig);
  }

}
