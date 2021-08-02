import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/interfaces/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  submitted = false; 
  id : number; 
  product  : Product;  

  constructor(private productService : ProductService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      
      params =>{
        this.id = parseInt(params.get('id')); 
      } 
    );
    this.loadData(); 
  }
  loadData(){
    this.productService.getProductById(this.id).subscribe(
      data => this.product = data 
      
    )
  }
  handleSubmit(f: NgForm){
    this.productService.updateProduct(this.product).subscribe(
      data => {
        this.submitted = true; 
      }
    )
  }

}
