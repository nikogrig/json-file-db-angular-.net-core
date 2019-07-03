import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { AuthInterceptor } from 'src/app/interceptors/auth.interceptor';
import { ErrorInterceptor } from 'src/app/interceptors/error.interceptor';

@NgModule({
  declarations: [
      LoginComponent,
      RegisterComponent,
    ],
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    ToastrModule.forRoot(),
    ReactiveFormsModule,
    BrowserModule,
    RouterModule,
    HttpClientModule,
  ],
  providers: [
      AuthService,
     {
       provide: HTTP_INTERCEPTORS,
       useClass: AuthInterceptor,
       multi: true
     },
     {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  exports: [ ] 
})
export class AuthenticationModule { }
