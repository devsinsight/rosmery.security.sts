import { IEdit } from './interfaces/edit.interface';
import { IFilter } from './interfaces/filter.interface';
import { ILog } from './interfaces/log.interface';

export class GridModel implements IEdit, IFilter, ILog {
	_isEditMode: boolean = false;
	_isNew: boolean = false;
	_isUpdated: boolean = false;
	_isDeleted: boolean = false;
	_prevState: any = null;

	_defaultFieldName: string = '';

	_userId: number = 0;
	_createdDate: string;
	_updatedDate: string;
}