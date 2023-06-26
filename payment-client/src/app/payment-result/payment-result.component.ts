import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaymentResultService } from './paymentResult.service';

@Component({
  selector: 'app-payment-result',
  templateUrl: './payment-result.component.html',
  styleUrls: ['./payment-result.component.css']
})
export class PaymentResultComponent implements OnInit {
  paymentResult: any;

  constructor(private paymentResultService: PaymentResultService) { }

  ngOnInit() {
    this.paymentResult = this.paymentResultService.getPaymentResult();
    console.log('Payment result:', this.paymentResult);
  }
}