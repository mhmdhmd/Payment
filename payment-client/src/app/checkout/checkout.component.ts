import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CartService } from '../product-list/cart.service';
import { PaymentResultService } from '../payment-result/paymentResult.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})

export class CheckoutComponent {

  constructor(private cartService: CartService, private httpClient: HttpClient, private router: Router, private paymentResultService: PaymentResultService) { }

  card: any = {
    cardNumber: '2222-4107-4036-0010',
    expirationMonth: '03',
    expirationYear: '2030',
    securityCode: '737'
  };

  customer: any = {
    fName: 'Hamid',
    lName: 'Mohammadi',
    address: 'Tehran, Iran'
  }

  getTotalPrice() {
    let totalPrice = 0;
    for (const item of this.cartService.cartItems) {
      totalPrice += item.price;
    }

    return totalPrice;
  }

  pay() {
    const paymentUrl = environment.baseUrl + 'api/payment'; // Replace with your actual payment endpoint

    const cNumber = "test_" + this.card.cardNumber.replace(/-/g,"");
    const exMonth = "test_" + this.card.expirationMonth;
    const exYear = "test_" + this.card.expirationYear;
    const securityCode = "test_" + this.card.securityCode;

    const paymentData = {
      currency: 'EUR',
      amount: this.getTotalPrice(),
      description: `my store payment ${this.getTotalPrice()}`,
      cardNumber: cNumber,
      expiryMonth: exMonth,
      expiryYear: exYear,
      securityCode: securityCode,
      fName : this.customer.fName,
      lName : this.customer.lName,
      address : this.customer.address
    };

    this.httpClient.post(paymentUrl, paymentData)
      .subscribe(
        (response: any) => {
          // Handle success response
          console.log('Payment successful:', response);
          this.paymentResultService.setPaymentResult(response);
          this.cartService.clearCartItems();
          this.router.navigate(['/payment-result']);
          
        },
        (error: any) => {
          // Handle error response
          console.error('Payment failed:', error);
          this.paymentResultService.setPaymentResult(error);
          this.router.navigate(['/payment-result']);
        }
      );
  }
}
