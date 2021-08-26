import { OnInit } from '@angular/core';
import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from '../user.service';
import { IUser } from '../users/user.interface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit,OnDestroy{
  public _usersCollection :IUser[];
  public _userSubscribtion:Subscription;

  constructor(private userService:UserService){}

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
  
  ngOnDestroy(): void {
    this._userSubscribtion.unsubscribe();
  }
}
