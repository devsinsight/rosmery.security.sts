import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {
   }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    console.log('url: ', state.url);

    await this.authService.isLoggedIn().subscribe( user => {
      if (!user) {
        this.router.navigate(['/account/login']);
      }
    });

    return true;
  }


}
