import { Component, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  totalItem = 0;
  signedIn : boolean = false; 


  
  constructor(private cartService:  CartService, private authService : AuthService) { }

  ngOnInit(): void {
   
    this.cartService.getCount().subscribe(count => {
      
      console.log(count); 
      this.totalItem = count;
      }
    );
    this.signedIn = this.authService.isSignedIn(); 
   

    this.authService.loginChangedEvent.subscribe(signIn => this.signedIn = signIn); 
   
  }
  signOut() {
    this.authService.isSignedOut();
  }
  
  isAdmin(): boolean{
    var isAdmin: boolean = this.authService.isAdmin(); 
    return isAdmin;
  }
  

}
