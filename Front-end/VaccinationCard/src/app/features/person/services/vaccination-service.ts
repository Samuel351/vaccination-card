import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CreateVaccinationRequest } from '../models/createVaccinationRequest';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../shared/models/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class VaccinationService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = environment.apiUrl+"/Vaccination";

  createVaccination(createVaccination: CreateVaccinationRequest) : Observable<ApiResponse>{
    return this.http.post<ApiResponse>(this.apiUrl, createVaccination);
  }

  editVaccination(updateVaccinationRequest: VaccineDose){
    return this.http.put<ApiResponse>(this.apiUrl, updateVaccinationRequest);
  }

  deleteVaccination(vaccinationId: string) : Observable<ApiResponse>{
    return this.http.delete<ApiResponse>(this.apiUrl+"/"+vaccinationId);
  }
}
