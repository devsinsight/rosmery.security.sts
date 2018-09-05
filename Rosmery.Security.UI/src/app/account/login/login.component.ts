import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../common/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.startAuthentication();
  }

}
