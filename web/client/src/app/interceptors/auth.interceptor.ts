import { HttpResponse, HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	
	constructor(private router: Router, 
		private authService: AuthService, 
		private toastr: ToastrService
		){ }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {		
		request = request.clone({
			setHeaders: {
				'Content-Type': 'application/json',
			}
		});		
		return next.handle(request)
			.pipe(tap((response: HttpResponse<any>) => { //TODO: /register
				if (response instanceof HttpResponse && (response.url.endsWith('login') || response.url.endsWith('register'))) {	
					this.saveDataStorage(response.body);
				}
				
				if (response instanceof HttpResponse && response.url.endsWith('login')) {
                    this.toastr.success('You have successfully login!', `Hello ${response.body.firstname}!`)				
				}

				if (response instanceof HttpResponse && response.url.endsWith('register')) {
                    this.toastr.success('You have successfully register!', `Your new username is ${response.body.firstname}`)
				}
		}));
	}

    private saveDataStorage(data) {
		this.authService.authenticate(data);
        this.router.navigateByUrl('/');
	}
}