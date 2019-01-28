import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './models/user';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable()
export class AccountService {

  constructor(private http: HttpClient) { }

  createUser(user: User) {
    return this.http.post(environment.baseSeurityApiUrl + '/api/account/register', user)
    .pipe(
      map(response => response)

    );

  }
}
