import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterState } from '@angular/router';
import { PersonService } from '../../services/person-service';
import { PersonResponse } from '../../models/personResponse';
import { VaccinationResponse } from '../../models/vaccinationResponse';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";

@Component({
  selector: 'app-person-details',
  imports: [ButtonComponent, RouterLink],
  templateUrl: './person-details.html',
  styleUrl: './person-details.scss'
})
export class PersonDetails implements OnInit {
  private router = inject(ActivatedRoute);
  private personService = inject(PersonService);

  protected person?: PersonResponse;
  protected vaccinationCard?: VaccinationResponse;

  ngOnInit(): void {
    this.router.params.subscribe(params => {
      const id = params['id'];
      this.getPersonDetails(id);
      this.getPersonVaccinationCard(id);
    });
  }

  getPersonDetails(personId: string){
    this.personService.getPersonById(personId).subscribe({
      next: res =>{
        this.person = res;
      },
      error: error => {

      }
    })
  }

  getPersonVaccinationCard(personId: string){
    this.personService.getPersonVaccinationCard(personId).subscribe({
      next: res => {
        this.vaccinationCard = res;
      },
      error: error => {

      }
    })
  }
  }
