import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-callback',
  template: '<div>Please wait...</div>'
})
export class CallbackComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.completeAuthentication().then( () => {
      this.router.navigate(['/home']);
    });
  }

}
