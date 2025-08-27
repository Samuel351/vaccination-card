import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication-service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard {
  constructor(private authServiceUser: AuthenticationService) {}

  canActivate(): boolean {
    if (!this.authServiceUser.isUserLogged()) {
      this.authServiceUser.logout();
      return false;
    }
    return true;
  }
}
