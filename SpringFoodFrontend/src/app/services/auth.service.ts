import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterUserRequest } from '../interfaces/RegisterUserRequest';
import { UserCredentials } from '../interfaces/UserCredentials';
import { Output, EventEmitter } from '@angular/core';
import { LoginRequest } from '../interfaces/LoginRequest';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user: UserCredentials | null = null ;
  @Output() loginChangedEvent :  EventEmitter<boolean> = new EventEmitter<boolean>(); 

  baseUrl: string = "https://localhost:44349/api";
  httpOptions = {
    headers: new HttpHeaders(
      {
        "Content-Type": "application/json"
      }
    )
  }

  constructor(private http: HttpClient) { 
    
  }
  registerUser(user: RegisterUserRequest): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + "/user/register", user, this.httpOptions);
  }
  loginUser(user: LoginRequest) {
    return this.http.post<UserCredentials>(this.baseUrl + "/user/login", user, this.httpOptions).subscribe(us =>
      this.saveUserAndToken(us));
  }


  saveUserAndToken(creden: UserCredentials) {

    this.user = creden;
    this.loginChangedEvent.emit(true); 

  }

  isSignedIn(): boolean {
    return this.user != null;
  }

  isSignedOut(): void {
    this.user = null;
    this.loginChangedEvent.emit(false); 

  }

  isAdmin() : boolean {
    var isAdminChe : boolean =  this.user.isAdmin; 
    return isAdminChe; 
  }

}
