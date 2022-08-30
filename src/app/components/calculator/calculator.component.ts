import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/Models/Country';
import { Results } from 'src/app/Models/Results';
import { ApiDataService } from 'src/app/Services/api-data.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.css']
})
export class CalculatorComponent implements OnInit {

  //Declaration of variables to be used by component
  Form!: FormGroup;
  country!: Country[];
  result!: Results;
  success?: boolean = false;
  date = new Date();
  today!: any;
  pipe = new DatePipe('en-US');

  constructor(private fb: FormBuilder, private services: ApiDataService) { }

  ngOnInit(): void {
    this.services.getCountry().subscribe((data) => {this.country = data;
    console.log(this.country)});
    // this.country = this.services.getcountry();  
    
    // ###### form Values ###############

    this.Form = this.fb.group({
      checkoutDate: ['', [
        Validators.required,
      ]],
      returnDate: ['', [
        Validators.required,
      ]],
      id: [null, [
        Validators.required,
      ]],
    })
  }

  // Change Format function to change format of todat Date to compare with user inputs

  changeFormat(today: any){
    let ChangedFormat = this.pipe.transform(this.date, 'YYYY-MM-dd');
    this.today = ChangedFormat;
  }

  // Submit Function to submit User input and display result

  async submit() {

    // Calling changeFormat function
    this.changeFormat(this.date)

    // Alert Manipulations
    const checkoutDate = document.getElementById('checkoutDate')
    checkoutDate?.classList.add('d-none')
    const returnDate = document.getElementById('returnDate')
    returnDate?.classList.add('d-none')
    const id = document.getElementById('id')
    id?.classList.add('d-none')
    const invalid = document.getElementById('invalid')
    invalid?.classList.add('d-none')
    const cInvalid = document.getElementById('cInvalid')
    cInvalid?.classList.add('d-none')
    const rInvalid = document.getElementById('rInvalid')
    rInvalid?.classList.add('d-none')


    
    const formValues = this.Form.value;


    // Through Alerts Incase of wrong User Inputs

    if (formValues.checkoutDate == "") {
      checkoutDate?.classList.remove('d-none')
    }
    else if (formValues.checkoutDate > this.today ) {
      cInvalid?.classList.remove('d-none')
    }
    else if (formValues.returnDate == "") {
      returnDate?.classList.remove('d-none')
    }
    else if (formValues.returnDate > this.today) {
      rInvalid?.classList.remove('d-none')
    }
    else if (formValues.checkoutDate > formValues.returnDate) {
      invalid?.classList.remove('d-none')
    }
    else if (formValues.id == null) {
      id?.classList.remove('d-none')
    } 
    else {
      try {
        this.services.getResult(formValues).subscribe((data) => {this.result = data;
          console.log(this.result)});
        // this.result = this.services.getresult(formValues);
        this.success = true;
      }
      catch (error) {
        console.error(error);
      }
    }
  }
}
