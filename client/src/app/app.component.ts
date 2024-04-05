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

  public forecasts: WeatherForecast[] = [];

  constructor(private accountService:AccountService) {}

  ngOnInit() {
    this.setCurrentUser();
  }
  setCurrentUser() {
    //const user: User = JSON.parse(localStorage.getItem('user')!);
    const strUser = localStorage.getItem('user');
    if (!strUser) return;
    const user: User = JSON.parse(strUser);
    this.accountService.setCurrentUser(user);
  }

}
