import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from './shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit, OnDestroy {

  isLoggedIn: boolean;

  constructor(private authService: AuthService, private router: Router) {}

  async ngOnInit() {
    await this.authService.userLoadedEvent.subscribe( result => {
      this.isLoggedIn = !!result;
    });
  }

  ngOnDestroy(): void {
  }

  signout() {
    this.router.navigate(['/account/logout']);
  }

  signup () {
    this.router.navigate(['/account/create-user']);
  }
}
