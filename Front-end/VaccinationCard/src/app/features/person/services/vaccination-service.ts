import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VaccinationService {
  private http: HttpClient = inject(HttpClient);
  private apiUrl = environment.apiUrl+"/Vaccinaton";
}
