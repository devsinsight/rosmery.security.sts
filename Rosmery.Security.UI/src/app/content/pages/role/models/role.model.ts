export class RoleModel {
    id: string;
    name: string;
    description: string;
    hasUsers: boolean;
    isActive: boolean;

    clear(){
        this.id = '';
        this.name = '';
        this.description = '';
    }
}