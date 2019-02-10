import { Observable, of } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { RoleService } from '../services/role.service';
import { QueryParamsModel } from '../models/query-params.model';
import { GridDataSource } from './grid.datasource';
import { QueryResultsModel } from '../models/query-results.model';

export class RoleDataSource extends GridDataSource {
	constructor(
        private roleService: RoleService
        ) {
		super();
	}

	loadRoles(
		queryParams: QueryParamsModel
	) {
		this.loadingSubject.next(true);
		this.roleService.findRoles(queryParams).pipe(
			tap(res => {
				this.entitySubject.next(res.items);
				this.paginatorTotalSubject.next(res.totalCount);
			}),
			catchError(err => of(new QueryResultsModel([], err))),
			finalize(() => this.loadingSubject.next(false))
		).subscribe();
	}
}
