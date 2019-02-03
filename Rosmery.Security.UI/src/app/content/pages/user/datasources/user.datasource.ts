import { Observable, of } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { UserService } from '../services/user.service';
import { QueryParamsModel } from '../models/query-params.model';
import { GridDataSource } from './grid.datasource';
import { QueryResultsModel } from '../models/query-results.model';

export class UserDataSource extends GridDataSource {
	constructor(
        private userService: UserService
        ) {
		super();
	}

	loadUsers(
		queryParams: QueryParamsModel
	) {
		this.loadingSubject.next(true);
		this.userService.findUsers(queryParams).pipe(
			tap(res => {
				this.entitySubject.next(res.items);
				this.paginatorTotalSubject.next(res.totalCount);
			}),
			catchError(err => of(new QueryResultsModel([], err))),
			finalize(() => this.loadingSubject.next(false))
		).subscribe();
	}
}
