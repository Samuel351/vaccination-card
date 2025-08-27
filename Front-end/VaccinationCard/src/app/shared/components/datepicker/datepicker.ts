import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input, AfterViewInit, Injector, Output, EventEmitter } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-datepicker',
  imports: [CommonModule, MatDatepickerModule],
  templateUrl: './datepicker.html',
  styleUrl: './datepicker.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => Datepicker),
      multi: true
    }
  ]
})
export class Datepicker implements ControlValueAccessor, AfterViewInit {
  @Input() label?: string;
  @Input() datePlaceholder?: string;
  @Input() timePlaceholder?: string;
  @Input() errorMessage?: string;
  @Input() blockFutureDates: boolean = false;

  dateValue: string = '';
  timeValue: string = '';
  disabled: boolean = false;
  touched: boolean = false;
  ngControl: NgControl | null = null;

  @Output() seletectedDate = new EventEmitter<string>();


  private onChange = (value: string) => {};
  private onTouched = () => {};

  constructor(private injector: Injector) {}

  ngAfterViewInit() {
    // Obtém o NgControl após a inicialização para evitar dependência circular
    this.ngControl = this.injector.get(NgControl, null);
  }

  // Getter para verificar se o input de tempo deve estar desabilitado
  get isTimeDisabled(): boolean {
    return this.disabled || !this.dateValue;
  }

  // Getter para verificar se tem erro
  get hasError(): boolean {
    const hasFormControlErrors = !!(this.ngControl?.invalid && (this.ngControl?.touched || this.touched));
    
    // Validação personalizada para datas/horas futuras
    let hasCustomErrors = false;
    if (this.touched || this.ngControl?.touched) {
      hasCustomErrors = this.isFutureDateTime();
    }
    
    return hasFormControlErrors || hasCustomErrors;
  }

  // Getter para mensagem de erro
  get currentErrorMessage(): string {
    if (this.ngControl?.errors && (this.ngControl?.touched || this.touched)) {
      const errors = this.ngControl.errors;
      
      if (errors['required']) {
        return 'Este campo é obrigatório';
      }
    }
    
    // Validação personalizada para datas/horas futuras
    if (this.touched || this.ngControl?.touched) {
      if (this.blockFutureDates && this.isFutureDate()) {
        return 'Não é possível selecionar datas futuras';
      }
    }
    
    return this.errorMessage || '';
  }

  // Getter para o valor máximo da data (hoje)
  get maxDate(): string {
    if (this.blockFutureDates) {
      const today = new Date();
      return today.toISOString().split('T')[0];
    }
    return '';
  }


  private isToday(): boolean {
    if (!this.dateValue) return false;
    
    const selectedDate = new Date(this.dateValue);
    const today = new Date();
    
    return selectedDate.toDateString() === today.toDateString();
  }

  private isFutureDate(): boolean {
    if (!this.blockFutureDates || !this.dateValue) return false;
    
    const selectedDate = new Date(this.dateValue);
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    
    return selectedDate > today;
  }

  private isFutureDateTime(): boolean {
    return this.isFutureDate();
  }

  onDateInput(event: Event): void {
    const target = event.target as HTMLInputElement;
    const previousDateValue = this.dateValue;
    this.dateValue = target.value;
    
    // Se estiver bloqueando datas futuras e selecionou uma data futura, limpa o campo
    if (this.blockFutureDates && this.isFutureDate()) {
      this.dateValue = '';
      target.value = '';
    }
    
    // Se a data mudou e havia um horário selecionado, verifica se ainda é válido
    if (previousDateValue !== this.dateValue && this.timeValue) {
      // Se não há mais data selecionada, limpa o horário
      if (!this.dateValue) {
        this.timeValue = '';
      }
    }
    
    this.seletectedDate.emit(this.dateValue);
    this.onChange(this.dateValue);
  }

  onBlur(): void {
    this.touched = true;
    this.onTouched();
  }
  writeValue(value: string): void {
    if (value) {
      const date = new Date(value);
      if (!isNaN(date.getTime())) {
        // Format date as YYYY-MM-DD
        this.dateValue = date.toISOString().split('T')[0];
        // Format time as HH:MM
        this.timeValue = date.toTimeString().slice(0, 5);
      }
    } else {
      this.dateValue = '';
      this.timeValue = '';
    }
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}