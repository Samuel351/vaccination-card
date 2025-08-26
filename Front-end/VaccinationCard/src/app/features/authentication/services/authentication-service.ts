import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { LoginRequest } from '../models/loginRequest';
import { Observable } from 'rxjs';
import { TokenResponse } from '../models/tokenResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = environment.apiUrl+"/Authentication";
  private router = inject(Router);

  login(loginRequest: LoginRequest) : Observable<TokenResponse>{
    return this.http.post<TokenResponse>(this.apiUrl+"/login", loginRequest);
  }

  setToken(token: string){
    localStorage.setItem("token", token);
  }

  getToken() : string | null{
    return localStorage.getItem("token");
  }

  isUserLogged(){
    return !!localStorage.getItem("token");
  }

  private removeToken(){
    localStorage.removeItem('token');
  }

  logout(){
    this.removeToken();
    this.router.navigate(['/login']);
  }
}
