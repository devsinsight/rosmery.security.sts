import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent implements OnInit {
  time: any;
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.endSignoutMainWindow().then( () => {
      this.router.navigate(['/home']);
    });
  }

}
