import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { LoginModel } from 'src/app/models/dtos.write';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private authService: AuthService) {
    this.model = new LoginModel('','');
  }
  
  model: LoginModel;

  login() {
    this.authService
      .login(this.model)
      .subscribe();
  }
}
