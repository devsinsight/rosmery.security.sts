import { Component, OnInit } from '@angular/core';
import { AuthService } from '../common/services/auth.service';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  claims: any;
  tokenHeader: any;
  response: any;

  constructor(private authService: AuthService, private http: HttpClient) { }

  ngOnInit() {
    this.claims = this.authService.getClaims();
    this.tokenHeader = this.authService.getAuthorizationHeaderValue();
  }

  test() {
    this.http.get('http://localhost:5001/identity').subscribe( response => {
      this.response = response;
    });
  }

}
