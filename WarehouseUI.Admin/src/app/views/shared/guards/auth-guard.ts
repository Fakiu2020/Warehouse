import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(public auth: AuthService, public router: Router,private authService: AuthService) {}
  canActivate(next: ActivatedRouteSnapshot ): boolean {

    if (!this.auth.isAuthenticated()) {
      this.router.navigate(['login']);
      this.auth.logOut();
      return false;
    }

    const roles = next.data  != null ? next.data.roles as Array<string> : null;

    if (roles) {
      const match =  this.authService.roleMatch(roles) ;
      if (match) {
        return true;
      } else {
        this.router.navigate(['401']);
      }
    }

    // const isAdminPage =
    //   this.auth.authentication &&
    //   this.auth.authentication.roles &&
    //   this.auth.authentication.roles.some((x) => x == 'AdminPage');

    return true;
  }
}
