import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {
   }

  canActivate() {
    const isLoggedIn = this.authService.isLoggedIn();

    isLoggedIn.subscribe( result => {
      if (!result) {
        this.router.navigate(['/account/login']);
      }
    });

    return isLoggedIn;
  }


}
