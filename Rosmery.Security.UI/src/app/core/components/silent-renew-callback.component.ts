import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-silent-renew-callback',
  template: '<div>Please wait...</div>'
})
export class SilentRenewCallbackComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    console.log('renew token');
    this.authService.startSilentSigninMainWindow();
  }

}
