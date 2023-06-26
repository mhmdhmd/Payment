import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-payment-history',
  templateUrl: './payment-history.component.html',
  styleUrls: ['./payment-history.component.css']
})
export class PaymentHistoryComponent implements OnInit{

  constructor(private httpClient: HttpClient){}

  paymenDetails : any[] = [];

  ngOnInit(): void {
    const paymentUrl = environment.baseUrl + 'api/Payment/get-history'; // Replace with your actual payment endpoint

    this.httpClient.get(paymentUrl)
      .subscribe(
        (response: any) => {
          // Handle success response
          console.log('Payment history successful:');
          console.table(response);
          this.paymenDetails = response.dataList;
        },
        (error: any) => {
          // Handle error response
          console.error('Payment history failed:', error);
        }
      );
  }
}
