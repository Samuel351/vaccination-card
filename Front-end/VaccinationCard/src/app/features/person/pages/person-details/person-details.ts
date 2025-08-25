import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterState } from '@angular/router';
import { PersonService } from '../../services/person-service';
import { PersonResponse } from '../../models/personResponse';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";
import { DatePipe } from '@angular/common';
import { ApiResponse } from '../../../../shared/models/apiResponse';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Modal } from '../../../../shared/components/modal/modal';

@Component({
  selector: 'app-person-details',
  imports: [ButtonComponent, RouterLink, DatePipe, Modal],
  templateUrl: './person-details.html',
  styleUrl: './person-details.scss'
})
export class PersonDetails implements OnInit {
  private router = inject(ActivatedRoute);
  private personService = inject(PersonService);
  private snackBar = inject(MatSnackBar);

  protected selectedVaccination?: VaccinationResponse;
  protected lastDose: number = 0;

  protected person?: PersonResponse;
  protected vaccinationCard?: VaccinationResponse[] = [];

  protected showRegisterVaccinationModal: boolean = false;
  protected showRegisterDateVaccinationModal: boolean = false;

  ngOnInit(): void {
    this.router.params.subscribe(params => {
      const id = params['id'];
      this.getPersonDetails(id);
      this.getPersonVaccinationCard(id);
    });
  }

  private getPersonDetails(personId: string){
    this.personService.getPersonById(personId).subscribe({
      next: res =>{
        this.person = res;
      },
      error: error => {
        var apiResponse = error.error as ApiResponse
        this.snackBar.open(apiResponse.message, 'Fechar', {duration: 2000});
      }
    })
  }

  private getPersonVaccinationCard(personId: string){
    this.personService.getPersonVaccinationCard(personId).subscribe({
      next: res => {
        this.vaccinationCard = res;
      },
      error: error => {
        var apiResponse = error.error as ApiResponse
        this.snackBar.open(apiResponse.message, 'Fechar', {duration: 2000});
      }
    })
  }

  onClickRegisterVaccination(vaccinationResponse: VaccinationResponse){
    this.selectedVaccination = vaccinationResponse;
    var vaccinationDoses = this.vaccinationCard?.find(x => x.vaccineId == vaccinationResponse.vaccineId)?.vaccineDoses.map(x => x.doseNumber) ?? [];
    this.lastDose = Math.max(...vaccinationDoses)
    this.showRegisterVaccinationModal = true;
  }

  onCloseRegisterVaccination(){
    this.showRegisterVaccinationModal = false;
  }
}
