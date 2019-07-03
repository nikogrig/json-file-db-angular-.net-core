import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {

  constructor(private router: Router, 
    private authService: AuthService, 
    private toastr: ToastrService) { 
  }

  title: string = "Angular App";
  //id: string;

  logout() {
    this.authService
        .logout()
        .subscribe();
        this.router.navigateByUrl('/login');
				this.toastr.success(`You successfully loged out!` , 'Come back later')
        localStorage.clear();
  }
}
