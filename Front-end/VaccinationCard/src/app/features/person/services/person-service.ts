import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, Query } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { PersonResponse } from '../models/personResponse';
import { ApiResponse } from '../../../shared/models/apiResponse';
import { CreatePersonRequest } from '../models/createPersonRequest';
import { EditPersonRequest } from '../models/editPersonRequest';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private http: HttpClient = inject(HttpClient);

  private apiUrl = environment.apiUrl+"/Person";

  getAllPersonsPaginated() : Observable<PersonResponse[]>{
    return this.http.get<PersonResponse[]>(this.apiUrl);
  }

  deletePersonById(personId: string) : Observable<ApiResponse>
  {
    return this.http.delete<ApiResponse>(this.apiUrl+"/"+personId);
  }

  getPersonVaccinationCard(personId: string) : Observable<VaccinationResponse[]>{
    return this.http.get<VaccinationResponse[]>(this.apiUrl+"/"+personId+"/vaccination-card");
  }

  getPersonById(personId: string) : Observable<PersonResponse>{
    return this.http.get<PersonResponse>(this.apiUrl+"/"+personId);
  }

  createPerson(createPersonRequest: CreatePersonRequest) : Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.apiUrl, createPersonRequest);
  }

  editPerson(editPersonRequest: EditPersonRequest) : Observable<ApiResponse>{
    return this.http.put<ApiResponse>(this.apiUrl, editPersonRequest);
  }
  
}
