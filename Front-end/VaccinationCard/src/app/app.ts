import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InputComponent } from './shared/components/input-component/input-component';
import { ButtonComponent } from './shared/components/button-component/button-component';
import { RegisterPerson } from './features/person/pages/register-person/register-person';

@Component({
  selector: 'app-root',
  imports: [RegisterPerson],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('VaccinationCard');
}
