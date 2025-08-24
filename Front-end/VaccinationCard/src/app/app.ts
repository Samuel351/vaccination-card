import { Component, signal } from '@angular/core';
import { ConsultPersons } from './features/person/pages/consult-persons/consult-persons';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/components/navbar-component/navbar-component';

@Component({
  selector: 'app-root',
  imports: [NavbarComponent, RouterOutlet],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
}
