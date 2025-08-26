// input.component.ts
import { CommonModule } from '@angular/common';
import { Component, Input, forwardRef, AfterViewInit, Injector } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskDirective } from "ngx-mask";

@Component({
  selector: 'app-input',
  templateUrl: './input-component.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  styleUrls: ['./input-component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true
    }
  ]
})
export class InputComponent implements ControlValueAccessor, AfterViewInit {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() type: string = 'text';
  @Input() invalidMessage: string = "";

  value: string = '';
  disabled = false;
  touched = false;
  ngControl: NgControl | null = null;

  onChange = (value: any) => {};
  onTouched = () => {};

  constructor(private injector: Injector) {}

  ngAfterViewInit() {
    this.ngControl = this.injector.get(NgControl, null);
  }

  // Getter para verificar se o campo é inválido e foi tocado
  get invalid(): boolean {
    const hasFormControlErrors = !!(this.ngControl?.invalid && (this.ngControl?.touched || this.touched));
  
    return hasFormControlErrors;
  }

  // Getter para obter a mensagem de erro do FormControl
  get errorMessage(): string {
    if (this.ngControl?.errors && (this.ngControl?.touched || this.touched)) {
      const errors = this.ngControl.errors;
      
      // Verifica diferentes tipos de erro e retorna mensagens personalizadas
      if (errors['required']) {
        return this.invalidMessage || 'Este campo é obrigatório';
      }
      if (errors['email']) {
        return 'Email inválido';
      }
      if (errors['minlength']) {
        return `Mínimo de ${errors['minlength'].requiredLength} caracteres`;
      }
      if (errors['maxlength']) {
        return `Máximo de ${errors['maxlength'].requiredLength} caracteres`;
      }
      if (errors['pattern']) {
        return 'Formato inválido';
      }
      
      // Retorna a primeira chave de erro se não houver tratamento específico
      return this.invalidMessage || `Erro: ${Object.keys(errors)[0]}`;
    }
    
    return this.invalidMessage;
  }

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  handleInput(event: Event) {
    const input = event.target as HTMLInputElement;
    console.log(input.value)
    let inputValue = input.value;
    this.value = inputValue;
    this.onChange(this.value);
  }

  handleBlur() {
    this.touched = true;
    this.onTouched();
  }
}