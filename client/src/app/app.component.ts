import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Dating App';
  users: any;

  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient,private accountService:AccountService) {}

  ngOnInit() {
    this.getUsers();
    this.setCurrentUser();
  }
  setCurrentUser() {
    //const user: User = JSON.parse(localStorage.getItem('user')!);
    const strUser = localStorage.getItem('user');
    if (!strUser) return;
    const user: User = JSON.parse(strUser);
    this.accountService.setCurrentUser(user);
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete:()=>console.log("Request complete.")
    });
  }

}
