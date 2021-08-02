import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/interfaces/Order';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-single-order',
  templateUrl: './single-order.component.html',
  styleUrls: ['./single-order.component.css']
})
export class SingleOrderComponent implements OnInit {
  order! : Order;
  id : number = 0;

  constructor(private productService : ProductService, private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(paramType => {
      this.id = paramType.id;
    });
    
    this.productService.getOrderById(this.id).subscribe(order => {
      this.order = order;
    });
  }
} 
