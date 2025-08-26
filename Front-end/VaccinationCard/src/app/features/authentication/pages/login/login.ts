import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputComponent } from "../../../../shared/components/input-component/input-component";
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";
import { AuthenticationService } from '../../services/authentication-service';
import { LoginRequest } from '../../models/loginRequest';
import { handleApiError } from '../../../../shared/utils/apiHandleError';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Loader } from "../../../../shared/components/loader/loader";
import { finalize } from 'rxjs';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, InputComponent, ButtonComponent, ReactiveFormsModule, Loader],
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private authService = inject(AuthenticationService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(8)])
  });

  protected isLoading: boolean = false;

  onSubmit() {
    var request = this.form.getRawValue() as LoginRequest;
    this.sendLoginRequest(request);
  }

  private sendLoginRequest(loginRequest: LoginRequest){
    this.isLoading = true;
    this.authService.login(loginRequest).pipe(finalize(() => this.isLoading = false)).subscribe({
      next: res => {
        this.authService.setToken(res.token);
        this.router.navigate(['/pessoas'])
      },
      error: error => handleApiError(this.snackBar, error)
    }) 
  }

  onCreateAccount(){
    this.router.navigate(['/criar-usuario']);
  }
}