import { Component, OnInit } from '@angular/core';
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

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  test() {
    this.http.get('http://localhost:5001/identity')
      .subscribe( response => {
        this.response = response;
      }, error => {
        this.response = error;
      });
  }

}
