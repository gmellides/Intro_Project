import { Component, OnInit,Input, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/user.service';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit,AfterViewInit {
  @Input() public isMiniTable = false;
  @Input() private users:IUser[];
  private _userSubscribtion : Subscription;
  private usersDataSource : MatTableDataSource<IUser>;

  // @ViewChild(MatSort) sort: MatSort;
  // @ViewChild(MatPaginator) paginator:MatPaginator;

  constructor(private userService:UserService) { }

  displayedColumns = ["name", "Surname","BirthDate","email"];

  ngOnInit() {
    if(!this.isMiniTable){
      this._userSubscribtion = this.userService.getAllUsers().subscribe(
        {
          next: x => this.users?console.log(this.users):this.users = x,
          error: err => console.log("Error occured in User service")
        }
      );
    }
  }

  ngAfterViewInit() {
    this.usersDataSource = new MatTableDataSource<IUser>(this.users);
  }

  // ngAfterContentChecked(){
  //   this.usersDataSource.sort = this.sort;
  //   this.usersDataSource.paginator = this.paginator;
  // }

}
