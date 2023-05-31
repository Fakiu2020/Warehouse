import {Component} from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../views/shared/services/auth.service';
import { navItems } from '../../_nav';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
  public sidebarMinimized = false;
  public navItems = navItems;
  userName = null;
  constructor(private authService: AuthService, private router: Router) {
    if(this.authService.authentication){
      this.userName = (this.authService.authentication.userName)
    }
    

  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout(){
    this.authService.logOut();
    this.router.navigate(['login']);
  }
}
