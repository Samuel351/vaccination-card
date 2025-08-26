import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PersonService } from '../../services/person-service';
import { PersonResponse } from '../../models/personResponse';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Modal } from '../../../../shared/components/modal/modal';
import { Datepicker } from "../../../../shared/components/datepicker/datepicker";
import { FormsModule } from '@angular/forms';
import { CreateVaccinationRequest } from '../../models/createVaccinationRequest';
import { VaccinationService } from '../../services/vaccination-service';
import { VaccineService } from '../../../vaccine/services/vaccine-service';
import { Option, Dropdown } from '../../../../shared/components/dropdown/dropdown';
import { Loader } from '../../../../shared/components/loader/loader';
import { Timepicker } from '../../../../shared/components/timepicker/timepicker';
import { handleApiError } from '../../../../shared/utils/apiHandleError';

@Component({
  selector: 'app-person-details',
  imports: [ButtonComponent, RouterLink, DatePipe, Modal, Datepicker, FormsModule, Dropdown, Loader, Timepicker],
  templateUrl: './person-details.html',
  styleUrl: './person-details.scss'
})
export class PersonDetails implements OnInit {
  private router = inject(ActivatedRoute);
  private personService = inject(PersonService);
  private snackBar = inject(MatSnackBar);
  private vaccinationService = inject(VaccinationService);
  private vaccineService = inject(VaccineService);

  protected selectedVaccination?: VaccinationResponse;
  protected lastDose: number = 0;
  protected isLoading: boolean = false;

  protected person?: PersonResponse;
  protected vaccinationCard?: VaccinationResponse[] = [];

  protected showRegisterVaccinationModal: boolean = false;
  protected showRegisterDateVaccinationModal: boolean = false;
  protected showConfirmDeleteVaccination: boolean = false;
  protected showNewVaccinationModal: boolean = false;

  protected applicationDate?: string = undefined;
  protected applicationHour?: string = undefined;
  protected vaccinesOption: Option[] = [];

  protected personId?: string;
  protected selectedDose?: VaccineDose;
  protected vaccineId?: string = undefined;

  ngOnInit(): void {
    this.router.params.subscribe(params => {
      this.personId = params['id'];
      this.getPersonDetails(this.personId!);
      this.getPersonVaccinationCard(this.personId!);
    });
  }

  private getVaccines(){
    this.vaccineService.getAllVaccines().subscribe({
      next: res => {
        this.vaccinesOption = res.map(x => { return {name: x.name, value: x.vaccineId, disabled: this.vaccinationCard?.find(y => y.vaccineId == x.vaccineId) != null}});
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  private getPersonDetails(personId: string){
    this.personService.getPersonById(personId).subscribe({
      next: res =>{
        this.person = res;
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  private getPersonVaccinationCard(personId: string){
    this.personService.getPersonVaccinationCard(personId).subscribe({
      next: res => {
        this.vaccinationCard = res;
        this.getVaccines();
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  onClickRegisterVaccination(vaccinationResponse: VaccinationResponse){
    this.selectedVaccination = vaccinationResponse;
    var vaccinationDoses = this.vaccinationCard?.find(x => x.vaccineId == vaccinationResponse.vaccineId)?.vaccineDoses.map(x => x.doseNumber) ?? [];
    this.lastDose = Math.max(...vaccinationDoses)
    this.showRegisterVaccinationModal = true;
  }

  onContinueRegisterVaccination(){
    this.showRegisterDateVaccinationModal = true;
  }

  onCloseRegisterVaccination(){
    this.showRegisterVaccinationModal = false;
    this.showRegisterDateVaccinationModal = false;
    this.resetValues();
  }

  private resetValues(){
    this.vaccineId = undefined;
    this.applicationDate = undefined;
    this.applicationHour = undefined;
  }

  onSaveRegisterVaccination(){
    var createVaccinationRequest: CreateVaccinationRequest = {
      doseNumber: this.lastDose+1,
      personId: this.person?.personId!,
      vaccineId: this.selectedVaccination?.vaccineId!,
      applicationDate: this.applicationDate+"T"+this.applicationHour
    }

    this.saveVaccination(createVaccinationRequest);
  }

  private saveVaccination(createVaccinationRequest: CreateVaccinationRequest){
    this.vaccinationService.createVaccination(createVaccinationRequest).subscribe({
      next: res => {
        this.applicationDate = undefined;
        this.getPersonVaccinationCard(createVaccinationRequest.personId);
        this.snackBar.open(res.message, 'Fechar', {duration: 2000});
        this.showRegisterDateVaccinationModal = false;
        this.showRegisterVaccinationModal = false;
        this.showNewVaccinationModal = false;
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  private deleteVaccination(vaccinationId: string){
    this.vaccinationService.deleteVaccination(vaccinationId).subscribe({
      next: res => {
        this.getPersonVaccinationCard(this.personId!);
        this.snackBar.open(res.message, 'Fechar', {duration: 2000});
        this.showConfirmDeleteVaccination = false;
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  onClickDeleteVaccination(vaccineDose: VaccineDose, vaccination: VaccinationResponse){
    this.selectedDose = vaccineDose;
    this.showConfirmDeleteVaccination = true;
    this.selectedVaccination = vaccination;
  }

  onCloseDeleteVaccination(){
    this.selectedDose = undefined;
    this.showConfirmDeleteVaccination = false;
    this.selectedVaccination = undefined;
  }

  onConfirmDeleteVaccination(){
    this.deleteVaccination(this.selectedDose?.vaccinationId!);
  }

  onCancelDeleteVaccination(){
    this.showConfirmDeleteVaccination = false;
    this.vaccinationCard = undefined;
  }

  onNewVaccinationRegister(){
    this.showNewVaccinationModal = true;
  }

  onCloseNewVaccinationRegister(){
    this.applicationDate = undefined;
    this.showNewVaccinationModal = false;
    this.resetValues();
  }

  onSaveNewVaccinationRegister(){
    var createVaccinationRequest: CreateVaccinationRequest = {
      doseNumber: 1,
      personId: this.personId!,
      vaccineId: this.vaccineId!,
      applicationDate: this.applicationDate+"T"+this.applicationHour
    }

    this.saveVaccination(createVaccinationRequest);
  }

  onSelectVaccine(vaccineId: string){
    this.vaccineId = vaccineId;
  }

}
