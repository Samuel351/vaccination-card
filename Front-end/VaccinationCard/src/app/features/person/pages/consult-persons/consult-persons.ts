import { Component, inject, OnInit } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { TableComponent } from '../../../../shared/components/table-component/table-component';
import { PersonService } from '../../services/person-service';
import { PaginatedResponse } from '../../../../shared/models/paginatedResponse';
import { PersonResponse } from '../../../../shared/models/personResponse';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";

@Component({
  selector: 'app-consult-persons',
  imports: [TableComponent, ButtonComponent],
  templateUrl: './consult-persons.html',
  styleUrl: './consult-persons.scss'
})
export class ConsultPersons implements OnInit{

  private personService = inject(PersonService);

  protected paginatedResponse?: PaginatedResponse<PersonResponse>;

  protected pageNumber: number = 1;
  protected pageSize: number = 10;
  protected query?: string = "";

  ngOnInit(): void {
    this.personService.getAllPersonsPaginated(this.pageNumber, this.pageSize, this.query).subscribe({
      next: res => {
        this.paginatedResponse = res;
        this.pageNumber = res.pageNumber;
        this.pageSize = res.pageSize;
      },
      error: error => {

      }
    })
  }
}
