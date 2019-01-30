import { Injectable, EventEmitter } from '@angular/core';
import { UserManager, UserManagerSettings, User, WebStorageStateStore } from 'oidc-client';
import { Router } from '@angular/router';
import { Observable, from } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private user: User = null;
  private manager: UserManager = new UserManager(this.getClientSettings());
  userLoadedEvent: EventEmitter<User> = new EventEmitter<User>();

  constructor(private router: Router) {
    this.manager.getUser()
      .then((user) => {
          this.user = user;
          this.userLoadedEvent.emit(user);
      });

    this.manager.events.addUserLoaded(user => {
      console.log('user loaded: ', user);
      this.user = user;
      this.userLoadedEvent.emit(user);
    });

    this.manager.events.addUserUnloaded((e) => {
      this.userLoadedEvent.emit(null);
    });

    this.manager.events.addAccessTokenExpiring( (e) => {
      console.log('access token expiring!');
      this.manager.removeUser();
    });

  }

  isLoggedIn(): Observable<boolean> {
      return from(this.manager.getUser())
            .pipe(map<User, boolean>((user) => !!user));
    }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return this.user ? `${this.user.token_type} ${this.user.access_token}` : '';
  }

  startSigninMainWindow(): Promise<User> {
    return this.manager.signinRedirect();
  }

  endSigninMainWindow(): Promise<void> {
      return this.manager.signinRedirectCallback().then(user => {
          console.log("EL USER:", user)
          this.user = user;
      });
  }

  startSignoutMainWindow(): Promise<any> {
    return this.manager.signoutRedirect();
  }

  endSignoutMainWindow(): Promise<void> {
    return this.manager.signoutRedirectCallback().then(function (resp) {
      console.log('signed out', resp);
    });
  }

  startSilentSigninMainWindow(): Promise<any> {
    return this.manager.signinSilentCallback();
  }

  getClientSettings(): UserManagerSettings {
    return {
        authority: environment.baseSeguritySTSUrl,
        client_id: 'rosmery-security',
        redirect_uri: environment.baseUrl + '/signin-callback',
        post_logout_redirect_uri: environment.baseUrl + '/signout-callback',
        silent_redirect_uri: environment.baseUrl +  '/silent-renew-callback',
        response_type: 'id_token token',
        scope: 'openid profile rosmery-security',
        filterProtocolClaims: true, 
        loadUserInfo: true,
        automaticSilentRenew: true,
        userStore: new WebStorageStateStore({ store: window.localStorage }),
    };
  }
}