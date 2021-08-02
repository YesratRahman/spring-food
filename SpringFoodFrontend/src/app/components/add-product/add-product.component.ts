import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/interfaces/Category';
import { Product } from 'src/app/interfaces/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  name: string = "";
  price: number | undefined;
  quantity: number | undefined;
  image: string = "";
  description: string = "";
  categories: Category[] = [];
  @Input('ngModel') categoryId: number = 0;


  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.productService.getAllCategories().subscribe(category => {
      this.categories = category;
      console.log(this.categories);
    })
  }
  addProduct() {
    if (this.name == null || this.name == undefined 
      ||this.image == null || this.image == undefined
      ||this.description == null || this.description == undefined 
      ||this.price == null || this.price == undefined
      ||this.quantity == null || this.quantity == undefined
    ) {
      alert("Please enter all the required information to add a product.");
    }
    else {
      let toAdd: Product = {
        name: this.name,
        price: this.price,
        quantity: this.quantity,
        image: this.image,
        description: this.description,
        categoryId: this.categoryId
      };
      this.productService.addProduct(toAdd).subscribe((_) => { this.router.navigate(["/allProducts"]) });
    }
  }
  onDropdownChange(id: number) {
    this.categoryId = id;

  }

}