import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER  } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRouting } from './app.routing';
import { AuthGuardService } from './shared/services/auth-guard.service';
import { AuthService } from './shared/services/auth.service';
import { SigninCallbackComponent } from './shared/components/signin-callback.component';
import { SignoutCallbackComponent } from './shared/components/signout-callback.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptorService } from './shared/services/auth-interceptor.service';
import { FormsModule } from '@angular/forms';
import { SilentRenewCallbackComponent } from './shared/components/silent-renew-callback.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninCallbackComponent,
    SignoutCallbackComponent,
    SilentRenewCallbackComponent,
    HomeComponent,
  ],
  imports: [
    BrowserModule,
    AppRouting,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    AuthService,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
