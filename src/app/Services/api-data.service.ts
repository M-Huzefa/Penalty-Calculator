import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Country } from '../Models/Country';
import { Input } from '../Models/Input';
import { Results } from '../Models/Results';

@Injectable({
  providedIn: 'root'
})
export class ApiDataService {

  constructor(private http: HttpClient ){ }

  // countrylist: Country[] = [
  //   {
  //     Id: 1,
  //     Name: "Pakistan"
  //   },
  //   {
  //     Id: 2,
  //     Name: "UAE"
  //   }
  // ]

  // result: Results = {
  //   businessDays: 17,
  //   penalty: "penalty is 15 USD"
  // }

  // getcountry(): Country[] {
  //   return this.countrylist;
  // } 
  // getresult(input : Input): Results {
  //   let sdate = input.checkoutDate;
  //   let edate = input.returnDate;
  //   let id = input.id;
  //   console.log(sdate);
  //   console.log(edate);
  //   console.log(Id);
    
  //   return this.result;
  // } 

  // Function to get data from DB to display country Name to dropdown

  getCountry(): Observable<Country[]> {
    let url: string = "https://localhost:44330/api/CountryData"
    return this.http.get<Country[]>(url);
  }

  // Function Take user Inputs and return Results of Calcultions to display

  getResult(input : Input): Observable<Results> {
    let posturl: string = "https://localhost:44330/api/CalculatedAmount"
    return this.http.post<Results>(posturl, input);
  }
}
