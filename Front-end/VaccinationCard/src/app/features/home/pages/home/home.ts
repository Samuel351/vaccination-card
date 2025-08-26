import { Component } from '@angular/core';
import { NavbarComponent } from '../../../../shared/components/navbar-component/navbar-component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [NavbarComponent, RouterOutlet],
  standalone: true,
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {

}
