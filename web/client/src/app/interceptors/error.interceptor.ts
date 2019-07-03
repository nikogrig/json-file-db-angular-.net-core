import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(catchError((err: HttpErrorResponse) => {
        switch (err.status) {
          case 400:
             this.toastr.error('Try again!');
             break;
          case 401:
            if (err.url.endsWith('register')) {
              this.toastr.error('Sorry, you don\'t have access! Try with another email or password')
              break;
            }
            this.toastr.error('Sorry, you don\'t have access!')
            this.router.navigateByUrl('/register');
            break;
          case 403:
            this.toastr.error('Sorry, you try to access the path who are forbidden')
            this.router.navigateByUrl('/login');
            break;
          case 500:
            this.toastr.error('The server is not found! Try later')
            this.router.navigateByUrl('/');
            break;
        }
        return throwError(err.message);
      }));
  }
}
