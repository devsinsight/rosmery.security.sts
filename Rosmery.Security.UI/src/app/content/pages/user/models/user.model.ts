export class UserModel {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    roles: string[];
    isActive: boolean;

    fullName(): string{
        return this.firstName + ' ' + this.lastName
    }

    clear(): void {
        this.id = '';
        this.userName = '';
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.phoneNumber ='';
        this.isActive = false;
    }
}
