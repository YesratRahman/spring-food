import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart } from 'src/app/interfaces/Cart';
import { Order } from 'src/app/interfaces/Order';
import { OrderDetails } from 'src/app/interfaces/OrderDetails';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  cart!: Cart; 
  // order! : Order;
  // id : number = 0;
    subTotal : number = this.cartService.getCart().totalPrice;
  tax : number = this.cartService.getCart().tax; 
  orderTotal : number = this.subTotal + this.tax;
  date : Date | undefined;
  orderDetails : OrderDetails[] = [];
  firstName : string = "";
  lastName : string = "";
  email : string = "";
  isLoading = false; 
  city:string = "";
  street: string ="";
  homeNumber: string ="";
  apartment: string ="";
  postalCode: number; 

  constructor(private productService: ProductService, private cartService: CartService,private route : ActivatedRoute,
    private router : Router) { 
      this.setCart()
    }

  ngOnInit(): void {
    // this.route.params.subscribe(paramType => {
    //   this.id = paramType.id;
    // });
    
    // this.productService.getOrderById(this.id);
  }
  createOrder() {

    let toAdd : Order = {
      name : this.firstName + " " + this.lastName,
      email : this.email,
      date : new Date(),
      orderTotal : this.orderTotal,
      city: this.city,
      street: this.street,
      homeNumber: this.homeNumber,
      apartment: this.apartment,
      postalCode: this.postalCode, 
      orderDetails : this.setOrderDetails(),
    } 

    this.productService.createOrder(toAdd).subscribe(order => {this.router.navigate([`/order/${order.id}`])});
    this.cartService.clearCart(); 
  }
  setOrderDetails() : OrderDetails[] {
    let orderDetails : OrderDetails [] = [];

    for(var i = 0; i < this.cartService.getCart().items.length; i++) {1
        let orderDetail : OrderDetails = { 
          productId : this.cartService.getCart().items[i].product.id, 
          quantity :  this.cartService.getCart().items[i].quantity
        }
      orderDetails.push(orderDetail);
    }

    return orderDetails;
  }

getNumberOfItems(){
  return this.cartService.getCart().items.length;
}
setCart(){
  this.cart = this.cartService.getCart();
  console.log(this.cart);
}

}
