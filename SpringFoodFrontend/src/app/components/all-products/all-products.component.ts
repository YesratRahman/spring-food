import { Component, Input, OnInit, Pipe } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/interfaces/Product';
import { ProductService } from 'src/app/services/product.service';
import { filter } from 'rxjs/operators';



@Component({
  selector: 'app-all-products',
  templateUrl: './all-products.component.html',
  styleUrls: ['./all-products.component.css']
})
export class AllProductsComponent implements OnInit {
  products : Product[] = []; 

  constructor(private produtService : ProductService, private router: Router, private route : ActivatedRoute) { }

  ngOnInit(): void {
      this.produtService.getAllProducts().subscribe(proList => {
        this.products = proList; 
      })
    
  }
  

}
