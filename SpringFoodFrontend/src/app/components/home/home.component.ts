import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from 'src/app/interfaces/Category';
import { Product } from 'src/app/interfaces/Product';
import { AuthService } from 'src/app/services/auth.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  
  categories : Category[] = []; 

  constructor(private produtService : ProductService, 
    private authService: AuthService, private route : ActivatedRoute) { }

  ngOnInit(): void {
         this.produtService.getAllCategories().subscribe(list => {
        this.categories = list; 
      })
    
    
  }
} 