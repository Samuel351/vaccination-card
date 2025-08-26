import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { handleApiError } from '../../../../../shared/utils/apiHandleError';
import { CommonModule } from '@angular/common';
import { ButtonComponent } from '../../../../../shared/components/button-component/button-component';
import { InputComponent } from '../../../../../shared/components/input-component/input-component';
import { Loader } from '../../../../../shared/components/loader/loader';
import { CreateUserRequest } from '../../../models/createUserRequest';
import { UserService } from '../../../services/user-service';

@Component({
  selector: 'app-create-user',
  imports: [CommonModule, FormsModule, InputComponent, ButtonComponent, ReactiveFormsModule, Loader],
  standalone: true,
  templateUrl: './create-user.html',
  styleUrl: './create-user.scss'
})
export class CreateUser {
  private userService = inject(UserService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(8)])
  });

  protected isLoading: boolean = false;

  onSubmit() {
    var request = this.form.getRawValue() as CreateUserRequest;
    this.sendCreateUserRequest(request);
  }

  private sendCreateUserRequest(userRequest: CreateUserRequest){
    this.isLoading = true;
    this.userService.createUser(userRequest).pipe(finalize(() => this.isLoading = false)).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 2000})
        setTimeout(() => {
          this.router.navigate(['/login'])
        }, 2000);
      },
      error: error => handleApiError(this.snackBar, error)
    }) 
  }

  onCancel()
  {
    this.router.navigate(['/login']);
  }
}
