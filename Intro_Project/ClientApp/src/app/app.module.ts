import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { MapComponent } from './map-page/map/map.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UsersComponent } from './users/users.component';
import { UsersTableComponent } from './users/users-table/users-table.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import {MatSortModule} from '@angular/material/sort';
import { MapPageComponent } from './map-page/map-page.component';
import { MatDialogModule } from "@angular/material/dialog";
import { environment } from 'src/environments/environment';
import { StoreModule } from '@ngrx/store';
import { FormsModule } from '@angular/forms';
import { AddUserComponent } from './users/add-user/add-user.component';
import { userReducer } from './state/users.reducer';
import { EffectsModule } from '@ngrx/effects';
import { UserDetailsComponent } from './users/user-details/user-details.component';

@NgModule({
  declarations: [	
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    MapComponent,
    UsersComponent,
    UsersTableComponent,
    MapPageComponent,AddUserComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatDialogModule,
    FormsModule,
    StoreDevtoolsModule.instrument({
      name:'Intro_Project Dev Tools',
      maxAge:25,
      logOnly:environment.production
    }),
    StoreModule.forRoot({ users:userReducer }),
    RouterModule.forRoot([
    { path: '' , component: HomeComponent, pathMatch: 'full' },
    { path: 'users' , component: UsersComponent },
    { path: 'map' , component: MapPageComponent },
    { path: 'userDetails/:userId' , component:UserDetailsComponent }
    ], { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule,
    EffectsModule.forRoot([])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
