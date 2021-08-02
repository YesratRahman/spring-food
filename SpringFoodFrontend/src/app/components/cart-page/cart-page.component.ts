import { Component, OnInit } from '@angular/core';
import { Cart } from 'src/app/interfaces/Cart';
import { CartProduct } from 'src/app/interfaces/CartProduct';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.css']
})
export class CartPageComponent implements OnInit {
  cart!: Cart; 

  constructor( private cartService: CartService) { 
    this.setCart();
  }
  ngOnInit(): void {
  }
  
  removeFromCart(cartProduct: CartProduct){
    this.cartService.removeFromCart(cartProduct.product.id); 

    this.setCart();
  }
  changeQuantity(cartProduct: CartProduct, quantityInString: string){
    const quantity = parseInt(quantityInString);
    this.cartService.changeQuantity(cartProduct.product.id, quantity);
    this.setCart();
  }
  setCart(){
    this.cart = this.cartService.getCart();
    console.log(this.cart);
  }
  clearCart(){
    this.cartService.clearCart();
    this.setCart(); 
  }

}
