import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../core/services/layout/subheader.service';
import * as objectPath from 'object-path';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';

@Component({
	selector: 'm-dashboard',
	templateUrl: './dashboard.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {

	public config: any;

	constructor(
		private router: Router,
		private layoutConfigService: LayoutConfigService,
		private subheaderService: SubheaderService,
		private http: HttpClient
	) {
	}

	ngOnInit(): void {}

	test() {
		this.http.get(environment.baseSeurityApiUrl + '/identity')
		  .subscribe( response => {
			alert(JSON.stringify(response));
		  }, error => {
			alert(error);
		  });
	  }
}
