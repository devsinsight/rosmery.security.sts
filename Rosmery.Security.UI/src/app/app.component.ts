import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from './common/services/auth.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styles: []
})
export class AppComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.userLoadedEvent.subscribe( user => {
      this.isLoggedIn = !!user;
    });

  }

  logout() {
    this.authService.startSignoutMainWindow();
  }

}
