import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CartProduct } from 'src/app/interfaces/CartProduct';
import { Order } from 'src/app/interfaces/Order';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: Order[] = [];

  constructor(private productService: ProductService) {


  }

  ngOnInit(): void {

    this.productService.getAllOrders().subscribe(
      
      orderList => this.orders = orderList
      );
      console.log(this.orders);

  }
}








// this.id = parseInt(this.route.snapshot.paramMap.get('id')!);

  // this.id = this.route.snapshot.paramMap.get('id')
//   this.route.params.subscribe(paramType => {
//     this.id = paramType['id'];
//     console.log(this.id);

// this.productService.getOrderById(this.id).subscribe(order => 
//     this.order = order); 
//     console.log(this.order);
//   } ); 
// }
