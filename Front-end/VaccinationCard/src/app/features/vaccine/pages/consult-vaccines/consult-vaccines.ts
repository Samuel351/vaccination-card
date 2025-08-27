import { Component, inject, OnInit } from '@angular/core';
import { TableComponent } from '../../../../shared/components/table-component/table-component';
import { ButtonComponent } from '../../../../shared/components/button-component/button-component';
import { Modal } from '../../../../shared/components/modal/modal';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VaccineResponse } from '../../models/vaccineResponse';
import { VaccineService } from '../../services/vaccine-service';
import { InputComponent } from '../../../../shared/components/input-component/input-component';
import { FormControl, NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateVaccineRequest } from '../../models/createVaccineRequest';
import { handleApiError } from '../../../../shared/utils/apiHandleError';
import { EditVaccineRequest } from '../../models/editVaccineRequest';

@Component({
  selector: 'app-consult-vaccines',
  imports: [TableComponent, ButtonComponent, Modal, InputComponent, ReactiveFormsModule],
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
  protected showEditModal: boolean = false;
  protected selectedVaccine?: VaccineResponse;

  protected formBuilder = inject(NonNullableFormBuilder);

  protected form = this.formBuilder.group({
    vaccineId: new FormControl<string | null>(null),
    name: new FormControl<string>('', [Validators.required, Validators.minLength(3)]),
    requiredDoses: new FormControl<number>(1, [Validators.required, Validators.min(1)])
  });

  ngOnInit(): void {
    this.getAllVaccines();
  }

  private getAllVaccines(){
    this.vaccineService.getAllVaccines().subscribe({
      next: res => {
        this.vaccines = res;
      },
      error: (error)  => handleApiError(this.snackBar, error)
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
      error: (error)  => handleApiError(this.snackBar, error)
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

  onEditRegisterClick(vaccine: VaccineResponse){
    this.showEditModal = true;
    this.form.patchValue(vaccine);
  }

  onCancelEditRegister(){
    this.showEditModal = false;
  }

  onSaveRegister(){
    var request = this.form.value as CreateVaccineRequest;
    this.createVaccine(request);
  }

  onEditRegister(){
    var request = this.form.value as EditVaccineRequest;
    this.editVaccine(request);
  }

  private resetForm(){
    this.form.reset();
    // Quando o usuário voltar para o modal o valor ficará igual a 1, já que o form feita resetado acima
    this.form.controls.requiredDoses.setValue(1);
  }

  private createVaccine(createVaccineRequest: CreateVaccineRequest){
    this.vaccineService.createVaccine(createVaccineRequest).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 1000});
        this.showRegisterModal = false;
        this.getAllVaccines();
        this.resetForm();
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }

  private editVaccine(editVaccineRequest: EditVaccineRequest){
    this.vaccineService.editVaccine(editVaccineRequest).subscribe({
      next: res => {
        this.snackBar.open(res.message, 'Fechar', {duration: 1000});
        this.showEditModal = false;
        this.getAllVaccines();
        this.resetForm();
      },
      error: (error)  => handleApiError(this.snackBar, error)
    })
  }
}
