import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class PaymentResultService {
    private paymentResult: any;

    setPaymentResult(result: any) {
        this.paymentResult = result;
    }

    getPaymentResult() {
        return this.paymentResult;
    }
}