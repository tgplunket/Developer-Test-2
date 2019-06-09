import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from '../../../node_modules/rxjs';

@Component({
  selector: 'app-fetch-weather',
  templateUrl: './fetch-weather.component.html'
})
    export class FetchWeatherComponent{

   

 public forecasts;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      http.get(baseUrl + 'api/WeatherData/WeatherForecasts')
          .subscribe(result => {
        this.forecasts = result;
    }, error => console.error(error));
  }
}


