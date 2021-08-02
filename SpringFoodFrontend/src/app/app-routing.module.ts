import { NgModule } from "@angular/core";
import { ProductComponent } from "./components/product/product.component";
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from "./components/home/home.component";
import { AllProductsComponent } from "./components/all-products/all-products.component";
import { CartPageComponent } from "./components/cart-page/cart-page.component";
import { CheckoutComponent } from "./components/checkout/checkout.component";
import { OrdersComponent } from "./components/orders/orders.component";
import { SingleOrderComponent } from "./components/single-order/single-order.component";
import { ProductCategoryComponent } from "./components/product-category/product-category.component";
import { AdminComponent } from "./components/admin/admin.component";
import { AddProductComponent } from "./components/add-product/add-product.component";
import { DeleteEditProductComponent } from "./components/delete-edit-product/delete-edit-product.component";
import { UserRegistrationComponent } from "./components/user-registration/user-registration.component";
import { UserLoginComponent } from "./components/user-login/user-login.component";
import { EditProductComponent } from "./components/edit-product/edit-product.component";
import { ContactPageComponent } from "./components/contact-page/contact-page.component";

const routes: Routes =[
    // { path: '', redirectTo: 'home', pathMatch: 'full'},
    { path: '', component: HomeComponent },
    { path: "allProducts", component: AllProductsComponent } , 
    {path: "search/:searchTerm", component: AllProductsComponent},
    {path: "product/:id", component: ProductComponent},
    {path: "cart-page", component: CartPageComponent}, 
    {path: "checkout", component: CheckoutComponent},
    {path: "orders", component: OrdersComponent},
    {path: 'order/:id', component: SingleOrderComponent },
    {path: 'product-by-category/:id', component: ProductCategoryComponent},
    {path: 'admin', component: AdminComponent}, 
    {path: 'admin/addProduct', component: AddProductComponent},
    {path: 'admin/delete-edit-product', component: DeleteEditProductComponent},
    {path: 'register', component: UserRegistrationComponent},
    {path: 'login', component: UserLoginComponent},
    {path: 'edit-product/:id', component: EditProductComponent},
    {path: 'contact', component:ContactPageComponent}






]; 
@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
  })
  export class AppRoutingModule {}