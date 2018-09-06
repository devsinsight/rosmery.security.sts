import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from './shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.userLoadedEvent.subscribe( user => {
      console.log('user: ', user);
      this.isLoggedIn = !!user;
    });
  }

  signin() {
    this.authService.startSigninMainWindow();
  }

  signout() {
    this.authService.startSignoutMainWindow();
  }

  signup () {
    this.router.navigate(['/account/create-user']);
  }
}
