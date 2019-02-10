import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { HttpUtilsService } from './http-utils.service';
import { RoleModel } from '../models/role.model';
import { environment } from '../../../../../environments/environment';

import { QueryResultsModel } from '../models/query-results.model';
import { QueryParamsModel } from '../models/query-params.model';

const API_USERS_URL = environment.baseSeurityApiUrl;

@Injectable()
export class RoleService {
    constructor(private http: HttpClient, private httpUtils: HttpUtilsService) { }

    getAllRoles(): Observable<RoleModel[]> {
        return this.http.get<RoleModel[]>(API_USERS_URL + '/api/role/GetRoles');
    }
    
	createRole(role: RoleModel): Observable<RoleModel> {
		const httpHeaders = this.httpUtils.getHTTPHeaders();
		return this.http.post<RoleModel>(API_USERS_URL + '/api/role/CreateRole', role, { headers: httpHeaders});
	}

	getRoleById(roleId: string): Observable<RoleModel> {
		return this.http.get<RoleModel>(API_USERS_URL + `/${roleId}`);
	}

	findRoles(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
		return this.http.get<RoleModel[]>(API_USERS_URL + '/api/role/GetRoles').pipe(
			mergeMap(res => {
				const result = this.httpUtils.baseFilter(res, queryParams, ['status', 'type']);
				return of(result);
			})
		);
	}

	updateRole(role: RoleModel): Observable<any> {
		const httpHeader = this.httpUtils.getHTTPHeaders();
		return this.http.put(API_USERS_URL + '/api/role/UpdateRole', role, { headers: httpHeader });
	}

	updateStatusForRole(roles: RoleModel[], isActive: boolean): Observable<any> {
		const tasks$ = [];
		for (let i = 0; i < roles.length; i++) {
			const _role = roles[i];
			_role.isActive = isActive;
			tasks$.push(this.updateRole(_role));
		}
		return forkJoin(tasks$);
	}

	deleteRole(roleId: string): Observable<RoleModel> {
		const url = `${API_USERS_URL}/api/role/DeleteRole/${roleId}`;
		return this.http.delete<RoleModel>(url);
	}

	deleteRoles(ids: string[] = []): Observable<any> {
		const tasks$ = [];
		const length = ids.length;
		for (let i = 0; i < length; i++) {
			tasks$.push(this.deleteRole(ids[i]));
		}
		return forkJoin(tasks$);
	}

	validateRoleName(roleName: string) {
		return this.http.get(`${API_USERS_URL}/api/role/ValidateRoleName/${roleName}`);
	}
}