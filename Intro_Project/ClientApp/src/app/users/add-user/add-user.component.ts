import { Component, OnInit } from '@angular/core';
import { IUser } from '../user.interface';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  public inputUser: IUser;

  constructor() { }

  ngOnInit() {
    
  }

}
