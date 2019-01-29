import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-signin-callback',
  template: '<div>Please wait...</div>'
})
export class SigninCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.endSigninMainWindow()
    .then( () => {
      this.router.navigate(['/']);
    });
  }

}
