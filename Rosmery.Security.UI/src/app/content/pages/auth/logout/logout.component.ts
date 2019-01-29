import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../core/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent implements OnInit {
  time: any;
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.startSignoutMainWindow();
  }

}
