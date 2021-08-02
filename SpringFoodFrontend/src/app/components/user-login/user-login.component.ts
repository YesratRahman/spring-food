import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/interfaces/LoginRequest';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  username : string = "";
  password : string = "";
  msg: string = null;


  constructor(private authService : AuthService, private router : Router) { }

  ngOnInit(): void {
  }

  submit(loginForm : NgForm) {
    if(loginForm.valid) {
      const user = loginForm.value;
    let toLogin : LoginRequest = {
      Username : this.username,
      Password : this.password,
    }

    this.authService.loginUser(toLogin);
    this.router.navigate([""]);
    this.msg = 'success';

  }
  }
  
}
