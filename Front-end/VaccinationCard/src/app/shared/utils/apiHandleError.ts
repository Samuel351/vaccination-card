// error-handler.utils.ts
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiResponse } from '../models/apiResponse';

export function handleApiError(snackBar: MatSnackBar, error: any) {
  const apiResponse = error.error as ApiResponse;

  snackBar.open(apiResponse.message, 'Fechar', { duration: 2000 });

  if (apiResponse.details && apiResponse.details.length > 0) {
    setTimeout(() => {
      snackBar.open(apiResponse.details.join(','), 'Fechar', { duration: 2000 });
    }, 1000);
  }
}
