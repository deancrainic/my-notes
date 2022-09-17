import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SidenavComponent } from './components/sidenav/sidenav.component';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import {AppRoutingModule} from "./app-routing/app-routing.module";
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import {ReactiveFormsModule} from "@angular/forms";
import {ToastrModule} from "ngx-toastr";

@NgModule({
  declarations: [
    AppComponent,
    SidenavComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent
  ],
    imports: [
        BrowserModule,
        MaterialModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        ReactiveFormsModule,
        ToastrModule.forRoot({
          timeOut: 5000,
          positionClass: 'toast-top-right'
        })
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
