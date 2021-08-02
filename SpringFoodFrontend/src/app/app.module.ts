import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { AllProductsComponent } from './components/all-products/all-products.component';
import { HomeComponent } from './components/home/home.component';
import { ProductComponent } from './components/product/product.component';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatIconModule} from '@angular/material/icon';
import {MatSidenavModule} from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterComponent } from './components/footer/footer.component';
import { CartPageComponent } from './components/cart-page/cart-page.component';
import { HeaderComponent } from './components/header/header.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { FormsModule } from '@angular/forms';
import { OrdersComponent } from './components/orders/orders.component';
import { MatCardModule } from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { SingleOrderComponent } from './components/single-order/single-order.component';
import { ProductCategoryComponent } from './components/product-category/product-category.component';
import { AddProductComponent } from './components/add-product/add-product.component';
import { AdminComponent } from './components/admin/admin.component';
import { DeleteEditProductComponent } from './components/delete-edit-product/delete-edit-product.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { UserLoginComponent } from './components/user-login/user-login.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './components/token.interceptor';
import { CartService } from './services/cart.service';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { ContactPageComponent } from './components/contact-page/contact-page.component';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductComponent,
    AllProductsComponent,
    FooterComponent,
    HeaderComponent,
    CartPageComponent,
    CheckoutComponent,
    OrdersComponent,
    SingleOrderComponent,
    ProductCategoryComponent,
    AdminComponent,
    AddProductComponent,
    DeleteEditProductComponent,
    UserRegistrationComponent,
    UserLoginComponent,
    EditProductComponent,
    ContactPageComponent  ],
  imports: [
    BrowserModule,
    AppRoutingModule, 
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    


  ],
  providers: [CartService, {provide : HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi : true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
