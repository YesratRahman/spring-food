import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterUserRequest } from 'src/app/interfaces/RegisterUserRequest';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {

  userName : string = ""; 
  userPassword : string = ""; 
  userEmail : string = ""; 
  confirmUserPassword: string = ""; 
  date : Date | undefined;
  constructor(private authService : AuthService, private router: Router) { }

  ngOnInit(): void {
  }
  submit(registerForm : NgForm){
    if(registerForm.valid) {
    let toAdd: RegisterUserRequest = {
      Username: this.userName,
      Email: this.userEmail,
      Password: this.userPassword,
      date: new Date()

    };
    this.authService.registerUser(toAdd).subscribe((_) => { this.router.navigate(["/login"])});
  }
} 

}
