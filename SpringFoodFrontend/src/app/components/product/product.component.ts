import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/interfaces/Product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
product! : Product; 
id : number = 0;
  constructor(private produtService : ProductService,private cartService: CartService,private router: Router, private route : ActivatedRoute) {} 
  ngOnInit(): void {
    this.route.params.subscribe(paramType => {
      this.id = paramType.id;
    });
    
    this.produtService.getProductById(this.id).subscribe(product => {
      this.product = product;
    }); 
  }

  addToCart(){
    this.cartService.addToCart(this.product); 
    this.router.navigateByUrl('/allProducts');
  };

}
