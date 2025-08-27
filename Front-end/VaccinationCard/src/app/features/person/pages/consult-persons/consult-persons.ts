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
import { Dropdown, Option } from "../../../../shared/components/dropdown/dropdown";
import { Loader } from '../../../../shared/components/loader/loader';
import { finalize } from 'rxjs';
import { NgxMaskPipe } from 'ngx-mask';
import { handleApiError } from '../../../../shared/utils/apiHandleError';
import { EditPersonRequest } from '../../models/editPersonRequest';

@Component({
  selector: 'app-consult-persons',
  imports: [TableComponent, ButtonComponent, Modal, RouterLink, InputComponent, ReactiveFormsModule, Dropdown, Loader, NgxMaskPipe],
  templateUrl: './consult-persons.html',
  styleUrl: './consult-persons.scss'
})
export class ConsultPersons implements OnInit{

  private personService = inject(PersonService);
  private snackBar = inject(MatSnackBar);

  protected personResponse?: PersonResponse[];
  protected isLoading: boolean = false;

  protected showConfirmModal: boolean = false;
  protected showRegisterModal: boolean = false;
  protected showEditModal: boolean = false;
  protected selectedPerson?: PersonResponse;

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    personId: new FormControl<string | null>(''),
    name: new FormControl<string>('', [Validators.required, Validators.minLength(3)]),
    cpf: new FormControl<string>('', [Validators.required]),
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl<string>('', [Validators.required]),
    gender: new FormControl('', [Validators.required]),
    age: new FormControl<number>(0, [Validators.required, Validators.min(0), Validators.max(120)])
  });

  ngOnInit(): void {
    this.getAllPersonsPaginated();
  }

  private getAllPersonsPaginated(){
    this.isLoading = true;
    this.personService.getAllPersonsPaginated().pipe(finalize(() => this.isLoading = false)).subscribe({
      next: res => {
        this.personResponse = res;
      },
      error: error => {
        this.personResponse = [];
        handleApiError(this.snackBar, error)
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
      error: (error)  => handleApiError(this.snackBar, error)
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
    this.createPerson(request);
  }

  onClickEditPerson(person: PersonResponse){
    this.form.patchValue(person);
    this.showEditModal = true;
  }

  onSaveEditRegister(){
    var request = this.form.value as EditPersonRequest;
    this.editPerson(request);
  }

  onCancelEdit(){
    this.showEditModal = false;
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
        this.resetForm();
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  private editPerson(editPersonRequest: EditPersonRequest){
    this.personService.editPerson(editPersonRequest).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 1000});
        this.showEditModal = false;
        this.getAllPersonsPaginated();
        this.resetForm();
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  onSelectGender(gender: string){
    this.form.controls.gender.setValue(gender);
  }

  GENDER_OPTIONS: Option[] = [
  { name: 'Feminino', value: 'Mulher', disabled: false },
  { name: 'Masculino', value: 'Homem', disabled: false },
  { name: 'Não-binário', value: 'Não binário', disabled: false },
  { name: 'Transgênero', value: 'Transgenero', disabled: false },
  { name: 'Outro', value: 'Outro', disabled: false },
  { name: 'Prefiro não informar', value: 'Não informado', disabled: false }
];
}



