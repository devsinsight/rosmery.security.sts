import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { HttpUtilsService } from './http-utils.service';
import { UserModel } from '../models/user.model';
import { QueryParamsModel } from '../models/query-params.model';
import { QueryResultsModel } from '../models/query-results.model';
import { environment } from '../../../../../environments/environment';

const API_USERS_URL = environment.baseSeurityApiUrl;

@Injectable()
export class UserService {
	constructor(private http: HttpClient, private httpUtils: HttpUtilsService) { }

	createUser(user: UserModel): Observable<UserModel> {
		const httpHeaders = this.httpUtils.getHTTPHeaders();
		return this.http.post<UserModel>(API_USERS_URL + '/api/user/CreateUser', user, { headers: httpHeaders});
	}

	getAllUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(API_USERS_URL + '/api/user/GetUsers');
	}

	getUserById(userId: string): Observable<UserModel> {
		return this.http.get<UserModel>(API_USERS_URL + `/${userId}`);
	}

	findUsers(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
		return this.http.get<UserModel[]>(API_USERS_URL + '/api/user/GetUsers').pipe(
			mergeMap(res => {
				const result = this.httpUtils.baseFilter(res, queryParams, ['status', 'type']);
				return of(result);
			})
		);
	}

	updateUser(user: UserModel): Observable<any> {
		const httpHeader = this.httpUtils.getHTTPHeaders();
		return this.http.put(API_USERS_URL + '/api/user/UpdateUser', user, { headers: httpHeader });
	}

	updateStatusForUser(users: UserModel[], isActive: boolean): Observable<any> {
		const tasks$ = [];
		for (let i = 0; i < users.length; i++) {
			const _user = users[i];
			_user.isActive = isActive;
			tasks$.push(this.updateUser(_user));
		}
		return forkJoin(tasks$);
	}

	deleteUser(userId: string): Observable<UserModel> {
		const url = `${API_USERS_URL}/api/user/DeleteUser/${userId}`;
		return this.http.delete<UserModel>(url);
	}

	deleteUsers(ids: string[] = []): Observable<any> {
		const tasks$ = [];
		const length = ids.length;
		for (let i = 0; i < length; i++) {
			tasks$.push(this.deleteUser(ids[i]));
		}
		return forkJoin(tasks$);
	}

	validateUserName(userName: string) {
		return this.http.get(`${API_USERS_URL}/api/user/ValidateUserName/${userName}`)
	}
}
