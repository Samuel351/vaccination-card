// input.component.ts
import { CommonModule } from '@angular/common';
import { Component, Input, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input-component.html',
  standalone: true,
  imports: [CommonModule],
  styleUrls: ['./input-component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => InputComponent),
      multi: true
    }
  ]
})
export class InputComponent implements ControlValueAccessor, Validator, OnInit {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() type: string = 'text';
  @Input() icon: string = '';
  @Input() required: boolean = false;
  @Input() minLength: number | null = null;
  @Input() maxLength: number | null = null;
  @Input() pattern: string = '';
  @Input() errorMessages: { [key: string]: string } = {};
  @Input() disabled: boolean = false;

  value: string = '';
  focused: boolean = false;
  touched: boolean = false;

  // ControlValueAccessor callbacks
  private onChange = (value: any) => {};
  private onTouched = () => {};

  ngOnInit() {
    // Mensagens de erro padrão
    this.errorMessages = {
      required: 'Este campo é obrigatório',
      minlength: `Mínimo de ${this.minLength} caracteres`,
      maxlength: `Máximo de ${this.maxLength} caracteres`,
      pattern: 'Formato inválido',
      ...this.errorMessages
    };
  }

  // ControlValueAccessor implementation
  writeValue(value: any): void {
    this.value = value || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  // Validator implementation
  validate(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    const errors: ValidationErrors = {};

    if (this.required && (!value || value.toString().trim() === '')) {
      errors['required'] = true;
    }

    if (this.minLength && value && value.toString().length < this.minLength) {
      errors['minlength'] = { requiredLength: this.minLength, actualLength: value.toString().length };
    }

    if (this.maxLength && value && value.toString().length > this.maxLength) {
      errors['maxlength'] = { requiredLength: this.maxLength, actualLength: value.toString().length };
    }

    if (this.pattern && value) {
      const regex = new RegExp(this.pattern);
      if (!regex.test(value.toString())) {
        errors['pattern'] = { requiredPattern: this.pattern, actualValue: value };
      }
    }

    return Object.keys(errors).length > 0 ? errors : null;
  }

  // Event handlers
  onInput(event: any): void {
    const value = event.target.value;
    this.value = value;
    this.onChange(value);
  }

  onFocus(): void {
    this.focused = true;
  }

  onBlur(): void {
    this.focused = false;
    this.touched = true;
    this.onTouched();
  }

  // Getter para verificar se tem erros
  get hasErrors(): boolean {
    return this.touched && this.getErrors().length > 0;
  }

  // Pega os erros do FormControl
  getErrors(): string[] {
    // Este método será utilizado quando o componente estiver dentro de um FormGroup
    // Para simplicidade, retornamos um array vazio aqui
    // Em um cenário real, você pegaria os erros do FormControl pai
    return [];
  }

  // Getter para classes CSS
  get inputClasses(): string {
    let classes = 'input-field';
    
    if (this.focused) classes += ' input-focused';
    if (this.hasErrors) classes += ' input-error';
    if (this.disabled) classes += ' input-disabled';
    if (this.icon) classes += ' input-with-icon';
    
    return classes;
  }

  get containerClasses(): string {
    let classes = 'input-container';
    
    if (this.focused) classes += ' container-focused';
    if (this.hasErrors) classes += ' container-error';
    
    return classes;
  }
}