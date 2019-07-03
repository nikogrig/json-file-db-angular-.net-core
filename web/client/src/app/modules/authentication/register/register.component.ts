import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { RegisterModel } from 'src/app/models/dtos.write';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  constructor(private authService: AuthService) { 
    this.model = new RegisterModel('','','','','','','');
  }

  model: RegisterModel;
  registerFailed: boolean;

  register() {
    delete this.model['confirmPassword'];
    this.authService
        .register(this.model)
        .subscribe(data =>  this.model = data,
          error => {
            console.log(error)
            delete this.model['username'];            
            delete this.model['email']; 
            delete this.model['password'];  
          });      
  }
}
