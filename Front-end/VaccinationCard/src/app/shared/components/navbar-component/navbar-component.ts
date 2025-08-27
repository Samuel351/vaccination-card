import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthenticationService } from '../../../features/authentication/services/authentication-service';

@Component({
  selector: 'app-navbar-component',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar-component.html',
  styleUrl: './navbar-component.scss'
})
export class NavbarComponent {
  private authenticationService = inject(AuthenticationService);

  onClose(){
    this.authenticationService.logout();
  }
}
