import { Component, OnInit,Input, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit {
  @Input() private isMiniTable = false;
  @Input() private users = [];
  private usersDataSource : MatTableDataSource<IUser> = null;
  private _userSubscribtion : Subscription;
   
  @ViewChild(MatPaginator,{static:true}) paginator:MatPaginator;

  constructor(private userService:UserService) { }

  ngOnInit() {
    if(!this.isMiniTable){
      this._userSubscribtion = this.userService.getAllUsers().subscribe(
        {
          next: x => this.users = x,
          error: err => console.log("Error occured in User service")
        }
      );

      this.usersDataSource = new MatTableDataSource(this.users);
    }
  }

}
