import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Product } from 'src/app/interfaces/Product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-delete-edit-product',
  templateUrl: './delete-edit-product.component.html',
  styleUrls: ['./delete-edit-product.component.css']
})
export class DeleteEditProductComponent implements OnInit {

  products: Product[] = [];
  @Output() notifyDelete: EventEmitter<number> = new EventEmitter<number>();


  constructor(private productService: ProductService) {

  }

  ngOnInit(): void {

    this.productService.getAllProducts().subscribe(proList => {
      this.products = proList;
    })


  }

  onDelete(product: Product): void {
    this.productService.deleteProduct(product.id).subscribe(result => {
      this.productService.getAllProducts().subscribe(prolist => {
        this.products = prolist;     

      })
    

    })

    
}




}

