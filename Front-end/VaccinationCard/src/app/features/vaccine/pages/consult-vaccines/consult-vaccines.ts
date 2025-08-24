import { Component, inject, OnInit } from '@angular/core';
import { TableComponent } from '../../../../shared/components/table-component/table-component';
import { ButtonComponent } from '../../../../shared/components/button-component/button-component';
import { RouterLink } from '@angular/router';
import { Modal } from '../../../../shared/components/modal/modal';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VaccineResponse } from '../../models/vaccineResponse';
import { VaccineService } from '../../services/vaccine-service';
import { InputComponent } from '../../../../shared/components/input-component/input-component';
import { FormControl, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateVaccineRequest } from '../../models/createVaccineRequest';
import { ApiResponse } from '../../../../shared/models/apiResponse';

@Component({
  selector: 'app-consult-vaccines',
  imports: [TableComponent, ButtonComponent, RouterLink, Modal, InputComponent, ReactiveFormsModule],
  standalone: true,
  templateUrl: './consult-vaccines.html',
  styleUrl: './consult-vaccines.scss'
})
export class ConsultVaccines implements OnInit {
  private snackBar = inject(MatSnackBar);
  private vaccineService = inject(VaccineService);

  protected vaccines?: VaccineResponse[];
  protected showConfirmModal: boolean = false;
  protected showRegisterModal: boolean = false;
  protected selectedVaccine?: VaccineResponse;

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    name: new FormControl<string>('', [Validators.required, Validators.minLength(3)]),
    requiredDoses: new FormControl<number>(1, [Validators.required])
  });

  ngOnInit(): void {
    this.getAllVaccines();
  }

  private getAllVaccines(){
    this.vaccineService.getAllVaccines().subscribe({
      next: res => {
        this.vaccines = res;
      },
      error: error => {
        console.log(error);
      }
    })
  }

  onClickDelete(selectedVaccine: VaccineResponse){
    this.showConfirmModal = true;
    this.selectedVaccine = selectedVaccine;
  }
  

  onConfirmeDelete()
  {
    this.vaccineService.deleteVaccineById(this.selectedVaccine!.vaccineId!).subscribe({
      next: res => {
        this.selectedVaccine = undefined;
        this.showConfirmModal = false;
        this.getAllVaccines();
        this.snackBar.open(res.message, 'Fechar', {duration: 10});
      },
      error: error => {
        console.log(error);
      }
    })
  }

  onCancelDelete(){
    this.selectedVaccine = undefined;
    this.showConfirmModal = false;
  }

  onRegisterClick(){
    this.showRegisterModal = true;
  }

  onCancelRegister(){
    this.showRegisterModal = false;
    this.resetForm();
  }

  onSaveRegister(){
    var request = this.form.value as CreateVaccineRequest;

    console.log(request);

    this.createVaccine(request);
    this.resetForm();
  }

  private resetForm(){
    this.form.reset();
  }

  private createVaccine(createVaccineRequest: CreateVaccineRequest){
    this.vaccineService.createVaccine(createVaccineRequest).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 1000});
        this.showRegisterModal = false;
        this.getAllVaccines();
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
