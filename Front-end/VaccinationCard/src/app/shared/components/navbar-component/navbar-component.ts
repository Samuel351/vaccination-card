import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar-component',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar-component.html',
  styleUrl: './navbar-component.scss'
})
export class NavbarComponent {
  isMenuOpen = false;

  links = [
    { label: 'Início', url: '/' },
    { label: 'Sobre', url: '/sobre' },
    { label: 'Contato', url: '/contato' },
  ];

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
