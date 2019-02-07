import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { HttpUtilsService } from './http-utils.service';
import { RoleModel } from '../models/role.model';
import { environment } from '../../../../../environments/environment';

const API_USERS_URL = environment.baseSeurityApiUrl;

@Injectable()
export class RoleService {
    constructor(private http: HttpClient, private httpUtils: HttpUtilsService) { }

    getAllRoles(): Observable<RoleModel[]> {
        return this.http.get<RoleModel[]>(API_USERS_URL + '/api/role/GetRoles');
	}
}