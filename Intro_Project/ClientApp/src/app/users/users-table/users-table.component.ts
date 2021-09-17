import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { IUserDTO } from '../user.interface';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})
export class UsersTableComponent implements OnInit,AfterViewInit {
  @Input() public users:IUserDTO[];

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit() {
  }

}
