import { RoleModel } from './role.model';

export class UserModel {
    userId: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    role: RoleModel;
    roleId: string;
    isActive: boolean;

    fullName(): string{
        return this.firstName + ' ' + this.lastName
    }

    clear(): void {
        this.userId = '';
        this.userName = '';
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.phoneNumber ='';
        this.role = new RoleModel();
    }
}
