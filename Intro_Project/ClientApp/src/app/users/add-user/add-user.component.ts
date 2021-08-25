import { Component, OnInit } from '@angular/core';
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

  constructor(private userService:UserService) { }

  ngOnInit() {
    // this._userSubscribtion = this.userService.getAllUsers().subscribe(
    //   {
    //     next: x => this._usersCollection = x,
    //     error: err => console.log("Error occured in User service")
    //   }
    // );
  }

}
