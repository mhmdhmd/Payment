import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { PaymentResultComponent } from './payment-result/payment-result.component';
import { PaymentHistoryComponent } from './payment-history/payment-history.component';

const routes: Routes = [
  { path: '', redirectTo: '/product-list', pathMatch: 'full' },
  { path: 'product-list', component: ProductListComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'payment-result', component: PaymentResultComponent },
  { path: 'payment-history', component: PaymentHistoryComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }