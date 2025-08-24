import { Component } from '@angular/core';
import { InputComponent } from '../../../../shared/components/input-component/input-component';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";

@Component({
  selector: 'app-register-person',
  imports: [InputComponent, ButtonComponent],
  standalone: true,
  templateUrl: './register-person.html',
  styleUrl: './register-person.scss'
})
export class RegisterPerson {

}
