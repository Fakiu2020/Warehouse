
import { Injectable } from '@angular/core';
import { HttpHandler, HttpRequest, HttpInterceptor, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Toast, ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService, private authservice: AuthService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse) {
          if (error.status === 401) {
            this.authservice.logOut();
            this.router.navigate(['login']);
            return throwError(error.statusText);
          }
          const applicationError = error.headers.get('Application-Error');
          if (applicationError) {
            return throwError(applicationError);
          }
          const serverError = error.error;
          let modalStateErrors = '';
          if (serverError && typeof serverError === 'object') {
            
            for (const key in serverError.error) {
              if (serverError.error[key]) {
               modalStateErrors += serverError.error[key] + '\n';
               //modalStateErrors = serverError.error_description || serverError.ExceptionMessage || serverError.errors.error[0];
              }
            }
          }
          return throwError(modalStateErrors || serverError || 'Server Error');
        }
      })
    );

  }
}
