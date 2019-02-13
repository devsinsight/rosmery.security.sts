import { Component, OnInit, ViewChild, ElementRef, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';
import { LayoutConfigService } from '../../services/layout-config.service';
import { ClassInitService } from '../../services/class-init.service';
import { DomSanitizer } from '@angular/platform-browser';
import * as objectPath from 'object-path';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-signin-callback',
  template: `<div #splashScreen class="m-splash-screen">
              <img [attr.src]="splashScreenImage" style="width: 90px; margin-bottom: 30px;" alt="Logo">
              <!--here we will have circle progress bar-->
              <mat-spinner diameter="40"></mat-spinner>
            </div>`
})
export class SigninCallbackComponent implements OnInit {

  @HostBinding('style') style: any;
  @ViewChild('splashScreen', {read: ElementRef})
	splashScreen: ElementRef;
	splashScreenImage: string;

  constructor(
    private authService: AuthService, 
    private layoutConfigService: LayoutConfigService,
		private classInitService: ClassInitService,
		private sanitizer: DomSanitizer,
    private router: Router,
    private http: HttpClient) { 

      this.layoutConfigService.onLayoutConfigUpdated$.subscribe(model => {
        this.classInitService.setConfig(model);
  
        this.style = '';
        if (objectPath.get(model.config, 'self.layout') === 'boxed') {
          const backgroundImage = objectPath.get(model.config, 'self.background');
          if (backgroundImage) {
            this.style = this.sanitizer.bypassSecurityTrustStyle('background-image: url(' + objectPath.get(model.config, 'self.background') + ')');
          }
        }
        
        this.splashScreenImage = objectPath.get(model.config, 'loader.image');
      });
  

    }

  ngOnInit() {
    this.authService.endSigninMainWindow()
    .then( () => {
        this.router.navigate(['/']);

    });
  }

}
