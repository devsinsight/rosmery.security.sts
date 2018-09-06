import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../shared/account.service';
import { User } from '../../shared/models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styles: []
})
export class CreateUserComponent implements OnInit {

  user: User = new User();

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
  }

  createUser() {
    this.accountService.createUser(this.user)
    .subscribe( (result: any) => {
      console.log(result);
      if (result.succeeded) {
        this.router.navigate(['/home']);
      }
    });
  }

}
