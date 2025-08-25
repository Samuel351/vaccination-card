import { Component, inject, OnInit } from '@angular/core';
import { TableComponent } from '../../../../shared/components/table-component/table-component';
import { PersonService } from '../../services/person-service';
import { PersonResponse } from '../../models/personResponse';
import { ButtonComponent } from "../../../../shared/components/button-component/button-component";
import { Modal } from '../../../../shared/components/modal/modal';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RouterLink } from '@angular/router';
import { InputComponent } from '../../../../shared/components/input-component/input-component';
import { FormControl, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreatePersonRequest } from '../../models/createPersonRequest';
import { ApiResponse } from '../../../../shared/models/apiResponse';

@Component({
  selector: 'app-consult-persons',
  imports: [TableComponent, ButtonComponent, Modal, RouterLink, InputComponent, ReactiveFormsModule],
  templateUrl: './consult-persons.html',
  styleUrl: './consult-persons.scss'
})
export class ConsultPersons implements OnInit{

  private personService = inject(PersonService);
  private snackBar = inject(MatSnackBar);

  protected personResponse?: PersonResponse[];

  protected showConfirmModal: boolean = false;
  protected showRegisterModal: boolean = false;
  protected selectedPerson?: PersonResponse;

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required, Validators.minLength(3)]),
    cpf: new FormControl<string>('', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl<string>('', [Validators.required]),
    gender: new FormControl('', [Validators.required]),
    age: new FormControl<number>(0, [Validators.required])
  });

  ngOnInit(): void {
    this.getAllPersonsPaginated();
  }

  private getAllPersonsPaginated(){
    this.personService.getAllPersonsPaginated().subscribe({
      next: res => {
        this.personResponse = res;
      },
      error: error => {
        var apiResponse = error.error as ApiResponse
        this.snackBar.open(apiResponse.message, 'Fechar', {duration: 2000});
      }
    })
  }

  onClickDelete(selectedPerson: PersonResponse){
    this.showConfirmModal = true;
    this.selectedPerson = selectedPerson;
  }

  onRegisterClick(){
    this.showRegisterModal = true;
  }

  onConfirmeDelete()
  {
    this.personService.deletePersonById(this.selectedPerson!.personId!).subscribe({
      next: res => {
        this.selectedPerson = undefined;
        this.showConfirmModal = false;
        this.getAllPersonsPaginated();
        this.snackBar.open(res.message, 'Fechar', {duration: 10});
      },
      error: error => {
        var apiResponse = error.error as ApiResponse
        this.snackBar.open(apiResponse.message, 'Fechar', {duration: 2000});
      }
    })
  }

  onCancelDelete(){
    this.selectedPerson = undefined;
    this.showConfirmModal = false;
  }

  onCancelRegister(){
    this.showRegisterModal = false;
    this.resetForm();
  }

  onSaveRegister(){
    var request = this.form.value as CreatePersonRequest;

    console.log(request);

    this.createPerson(request);
    this.resetForm();
  }

  private resetForm(){
    this.form.reset();
  }

  private createPerson(createVaccineRequest: CreatePersonRequest){
    this.personService.createPerson(createVaccineRequest).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 1000});
        this.showRegisterModal = false;
        this.getAllPersonsPaginated();
      },
      error: (error) => {
        var apiResponse = error.error as ApiResponse;

        this.snackBar.open(apiResponse.message, 'Fechar', {duration: 2000});

        setTimeout(() => {
          this.snackBar.open(apiResponse.details.join(','), 'Fechar', {duration: 2000});
        }, 1000);
      }
    })
  }
}
