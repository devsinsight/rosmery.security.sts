import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-silent-renew-callback',
  template: ''
})
export class SilentRenewCallbackComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.startSilentSigninMainWindow();
  }

}
