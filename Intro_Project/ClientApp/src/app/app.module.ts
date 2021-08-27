import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MapComponent } from './map-page/map/map.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { UsersComponent } from './users/users.component';
import { UsersTableComponent } from './users/users-table/users-table.component';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatInputModule} from '@angular/material/input';
import { MapPageComponent } from './map-page/map-page.component';
import { MatDialogModule } from "@angular/material/dialog";
import { environment } from 'src/environments/environment';
import { StoreModule } from '@ngrx/store';
// import { mapReducer } from './state/map.reducer';


@NgModule({
  declarations: [	
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MapComponent,
    UsersComponent,
    UsersTableComponent,
    MapPageComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    StoreDevtoolsModule.instrument({
      name:'Intro_Project Dev Tools',
      maxAge:25,
      logOnly:environment.production
    }),
    RouterModule.forRoot([
    { path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'users', component: UsersComponent },
    { path: 'map', component: MapPageComponent }
], { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule,
    // StoreModule.forFeature('users',usersReducer),
    // StoreModule.forFeature('map',mapReducer),
    // StoreModule.forRoot(reducer)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
