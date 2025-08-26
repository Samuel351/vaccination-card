import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthenticationService } from '../services/authentication-service';
import { tap } from 'rxjs';

export const tokenInterceptorFn: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthenticationService);
  const token = authService.getToken();

  console.log("request");
  const cloned = token ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }) : req;

  return next(cloned).pipe(
    tap({
      error: (err) => {
        if (err.status === 401) {
          authService.logout(); // Corrigido o nome do método
        }
      }
    })
  );
};
