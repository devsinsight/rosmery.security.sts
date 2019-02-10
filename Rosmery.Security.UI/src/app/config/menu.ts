// tslint:disable-next-line:no-shadowed-variable
import { ConfigModel } from '../core/interfaces/config';

// tslint:disable-next-line:no-shadowed-variable
export class MenuConfig implements ConfigModel {
	public config: any = {};

	constructor() {
		this.config = {
			header: {
				self: {},
				items: []
			},
			aside: {
				self: {},
				items: [
					{
						title: 'Dashboard',
						desc: 'Some description goes here',
						root: true,
						icon: 'flaticon-line-graph',
						page: '/',
						badge: {type: 'm-badge--danger', value: '2'},
						translate: 'MENU.DASHBOARD'
					},
					{section: 'Security'},
					{
						title: 'User',
						root: true,
						icon: '	flaticon-users',
						page: '/user'
					},
					{
						title: 'Role',
						root: true,
						icon: 'flaticon-map',
						page: '/role'
					},
					{
						title: 'Builder',
						root: true,
						icon: 'flaticon-settings',
						page: '/builder'
					}
				]
			}
		};
	}
}
