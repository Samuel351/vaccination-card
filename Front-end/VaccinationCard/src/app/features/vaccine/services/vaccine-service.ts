import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { VaccineResponse } from '../models/vaccineResponse';
import { ApiResponse } from '../../../shared/models/apiResponse';
import { CreateVaccineRequest } from '../models/createVaccineRequest';
import { EditVaccineRequest } from '../models/editVaccineRequest';

@Injectable({
  providedIn: 'root'
})
export class VaccineService {
  private http: HttpClient = inject(HttpClient);

  private apiUrl = environment.apiUrl+"/Vaccine";

  getAllVaccines() : Observable<VaccineResponse[]>{
    return this.http.get<VaccineResponse[]>(this.apiUrl);
  }

  getVaccineById(vaccineId: string) : Observable<VaccineResponse>{
    return this.http.get<VaccineResponse>(this.apiUrl+"/"+vaccineId);
  } 

  deleteVaccineById(vaccineId: string): Observable<ApiResponse>{
    return this.http.delete<ApiResponse>(this.apiUrl+"/"+vaccineId);
  }

  createVaccine(vaccine: CreateVaccineRequest) : Observable<ApiResponse>{
    return this.http.post<ApiResponse>(this.apiUrl, vaccine);
  }

  editVaccine(vaccine: EditVaccineRequest) : Observable<ApiResponse>{
    return this.http.put<ApiResponse>(this.apiUrl, vaccine);
  }
}
