import { Component, OnInit } from '@angular/core';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  
  public userDetails:IUser;
  constructor() { }

  ngOnInit() {
    console.log("USER DETAILS");

  }

}
