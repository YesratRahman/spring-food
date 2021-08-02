import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/interfaces/Category';
import { Product } from 'src/app/interfaces/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-category',
  templateUrl: './product-category.component.html',
  styleUrls: ['./product-category.component.css']
})
export class ProductCategoryComponent implements OnInit {

  products : Product[] = []; 
  categories : Category[] = []; 
  name : string = "";
  id : number = 0;
    constructor(private produtService : ProductService,private router: Router, private route : ActivatedRoute) {} 
    ngOnInit(): void {
      this.route.params.subscribe(paramType => {
        this.id = paramType.id;
      });
      
      this.produtService.getAllProductsByCategoryId(this.id).subscribe(product => {
        this.products = product;
        this.products.forEach(product => {
             product.category?.name ; 
          console.log(this.products[0].category?.name );
        });
        
        console.log(this.products)
      }); 
    }
  } 
