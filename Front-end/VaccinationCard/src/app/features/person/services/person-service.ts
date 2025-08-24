import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, Query } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { PaginatedResponse } from '../../../shared/models/paginatedResponse';
import { PersonResponse } from '../../../shared/models/personResponse';
import { Q } from '@angular/cdk/keycodes';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private http: HttpClient = inject(HttpClient);

  private apiUrl = environment.apiUrl+"/Person";

  getAllPersonsPaginated(pageNumber: number = 1, pageSize: number = 10, query?: string) : Observable<PaginatedResponse<PersonResponse>>{

    var params = new HttpParams();
    
    params.set("PageNumber", pageNumber)
    params.set("PageSize", pageNumber)

    if(query){
      params.set("Query", query);
    }

    return this.http.get<PaginatedResponse<PersonResponse>>(this.apiUrl, { params: params});
  }
  
}
