import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-signout-callback',
  template: '<div>Please wait...</div>'
})
export class SignoutCallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.endSignoutMainWindow()
    .then( () => {
      this.router.navigate(['/login']);
    });
  }

}
