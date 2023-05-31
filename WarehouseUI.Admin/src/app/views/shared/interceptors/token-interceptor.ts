import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Authentication } from '../models/authentication';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {

    constructor(private router: Router, private authService : AuthService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (this.router.url === '/login') {
            return next.handle(req);
        } 

        if (this.authService.authentication && this.authService.authentication.userName) {
            const modReq = req.clone({
                setHeaders: {
                    'Authorization': 'Bearer ' + this.authService.authentication.token
                }
            });
            return next.handle(modReq);
        }

    }
}