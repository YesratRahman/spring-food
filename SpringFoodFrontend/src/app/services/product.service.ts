import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Product } from '../interfaces/Product';
import {Observable, of} from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Order } from '../interfaces/Order';
import { Category } from '../interfaces/Category';


@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl: string = "https://localhost:44349/api";
  httpOptions ={
    headers: new HttpHeaders(
    {
      "Content-Type": "application/json"
    }
      ) 
  }

  constructor(private http: HttpClient) { }
  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "/product", this.httpOptions)
      .pipe(
        tap(x => console.log(x)),
        catchError(err => {
          console.log(err);
          let empty: Product[] = [];
          return of(empty);
        })
      );
  }

  getProductById(id : number) : Observable<Product> {
    return this.http.get<Product>(this.baseUrl + "/product/" + id);
  }

  addProduct(toAdd : Product) : Observable<Product> {
    return this.http.post<Product>(this.baseUrl + "/product", toAdd, this.httpOptions).pipe(
      tap(x => console.log(x)),
      catchError(err => {
        alert(err.error);
        let empty : Product = {
          name : "",
          price: 0,
          quantity: 0, 
          image: "", 
          description: "", 
        };
        return of(empty);
      })
    );
  }


  deleteProduct(productId: number){
    return this.http.delete<Product>(this.baseUrl + "/product/" + productId, this.httpOptions)
      .pipe(
        tap(x => console.log(x)),
        catchError(err => {
          console.log(err);
          return of(null);
        })
      );
  }
  updateProduct(toUpdate: Product): Observable<Product>  {
    return this.http.put<Product>(this.baseUrl + "/product" ,toUpdate,  this.httpOptions)
      .pipe(
        tap(x => console.log(x)),
        catchError(err => {
          console.log(err);
          let empty : Product = {
            name : "",
            price: 0,
            quantity: 0, 
            image: "", 
            description: "", 
            categoryId: 0 
          };
          return of(empty);
        })
      );
  }
  createOrder(toAdd : Order) : Observable<Order> {
    return this.http.post<Order>(this.baseUrl + "/order", toAdd, this.httpOptions).pipe(
      tap(x => console.log(x)),
      catchError(err => {
        alert(err.error);
        let empty : Order = {
          name : "",
          email : "",
          date : new Date(),
          orderTotal : 0,
          city: "",
          street : "",
          homeNumber: "",
          apartment: "",
          postalCode: 0,
          orderDetails : []
        };
        return of(empty);
      })
    );
  }
  getAllOrders() : Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + "/order")
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        let empty : Order[] = [];
        return of(empty);
      })
    );
  }

  getOrderById(id : number) : Observable<Order> {
    return this.http.get<Order>(this.baseUrl + "/order/" + id).pipe(
      tap(x => console.log(x)),
      catchError(err => {
        alert(err.error);
        let empty : Order = {
          name : "",
          email : "",
          date : new Date(),
          orderTotal : 0,
          city: "",
          street : "",
          homeNumber: "",
          apartment: "",
          postalCode: 0,
          orderDetails : []
        };
        return of(empty);
      })
    );;
  }
  getAllCategories() : Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + "/category")
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        let empty : Category[] = [];
        return of(empty);
      })
    );
  }
  getAllProductsByCategoryId(id: number) : Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "/category/"+ id)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        let empty : Product[] = [];
        return of(empty);
      })
    );
  }

  
  
}
