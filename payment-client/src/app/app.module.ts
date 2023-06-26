import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CartComponent } from './cart/cart.component';
import { ProductListComponent } from './product-list/product-list.component';
import { HeaderComponent } from './header/header.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { PaymentResultComponent } from './payment-result/payment-result.component';
import { AppRoutingModule } from './app-routing.module';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    ProductListComponent,
    HeaderComponent,
    CheckoutComponent,
    PaymentResultComponent,
    PaymentHistoryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
