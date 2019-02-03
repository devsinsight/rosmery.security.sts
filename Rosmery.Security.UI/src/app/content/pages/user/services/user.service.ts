import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { HttpUtilsService } from './http-utils.service';
import { UserModel } from '../models/user.model';
import { QueryParamsModel } from '../models/query-params.model';
import { QueryResultsModel } from '../models/query-results.model';
import { environment } from '../../../../../environments/environment';

const API_USERS_URL = environment.baseSeurityApiUrl + '/api/user/getUsers'

// Fake REST API (Mock)
// This code emulates server calls
@Injectable()
export class UserService {
	constructor(private http: HttpClient, private httpUtils: HttpUtilsService) { }

	// CREATE =>  POST: add a new customer to the server
	createUser(user: UserModel): Observable<UserModel> {
		// Note: Add headers if needed (tokens/bearer)
		const httpHeaders = this.httpUtils.getHTTPHeaders();
		return this.http.post<UserModel>(API_USERS_URL, user, { headers: httpHeaders});
	}

	// READ
	getAllUsers(): Observable<UserModel[]> {
        return this.http.get<UserModel[]>(API_USERS_URL  );
	}

	getCustomerById(userId: string): Observable<UserModel> {
		return this.http.get<UserModel>(API_USERS_URL + `/${userId}`);
	}

	// Method from server should return QueryResultsModel(items: any[], totalsCount: number)
	// items => filtered/sorted result
	findUsers(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
		// This code imitates server calls
		const url = API_USERS_URL;
		return this.http.get<UserModel[]>(API_USERS_URL).pipe(
			mergeMap(res => {
				const result = this.httpUtils.baseFilter(res, queryParams, ['status', 'type']);
				return of(result);
			})
		);
	}


	// UPDATE => PUT: update the customer on the server
	updateUser(customer: UserModel): Observable<any> {
		const httpHeader = this.httpUtils.getHTTPHeaders();
		return this.http.put(API_USERS_URL, customer, { headers: httpHeader });
	}

	// UPDATE Status
	updateStatusForUser(users: UserModel[], isActive: boolean): Observable<any> {
		const tasks$ = [];
		for (let i = 0; i < users.length; i++) {
			const _user = users[i];
			_user.isActive = isActive;
			tasks$.push(this.updateUser(_user));
		}
		return forkJoin(tasks$);
	}

	// DELETE => delete the customer from the server
	deleteUser(userId: string): Observable<UserModel> {
		const url = `${API_USERS_URL}/${userId}`;
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
}
