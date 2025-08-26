import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CreateUserRequest } from '../models/createUserRequest';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../shared/models/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = environment.apiUrl+"/User";

  createUser(createUserRequest: CreateUserRequest) : Observable<ApiResponse>{
    return this.http.post<ApiResponse>(this.apiUrl, createUserRequest);
  }

}
