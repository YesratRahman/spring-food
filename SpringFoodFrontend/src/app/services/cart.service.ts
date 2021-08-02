import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { Cart } from '../interfaces/Cart';
import { CartProduct } from '../interfaces/CartProduct';
import { Product } from '../interfaces/Product';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  private cart:Cart = new Cart(); 
  count = 0;
  private simpleObservable = new ReplaySubject<number>(1);
  simpleObservable$ = this.simpleObservable.asObservable();

  constructor() { 
  }

  addCount() {
    this.count+=1;
    this.simpleObservable.next(this.count)
  }
  removeCount() {
    if (this.count > 0) { this.count-=1 };
    this.simpleObservable.next(this.count)
  }
  clearCount() {
    this.count = 0;
    this.simpleObservable.next(this.count);
  }
  getCount(){
    return this.simpleObservable$;
  }

   addToCart(product: Product):void{
    let cartItem = this.cart.items.find(item => item.product.id === product.id);
    if(cartItem){
      this.changeQuantity(product.id, cartItem.quantity + 1);
      return;
    }
    this.cart.items.push(new CartProduct(product));
    this.addCount(); 
  }

  removeFromCart(productId:number | undefined): void{
    this.cart.items = this.cart.items.filter(item => item.product.id != productId);
    this.removeCount(); 

  }

  changeQuantity(productId:number | undefined, quantity:number){
    let cartItem = this.cart.items.find(item => item.product.id === productId);
    if(!cartItem) return;
    cartItem.quantity = quantity;
  }

  getCart():Cart{
    return this.cart;
  }
  clearCart() {
    this.cart.items = [];
    this.clearCount();
    return this.cart.items;
  }
  
}