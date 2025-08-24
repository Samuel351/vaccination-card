// input.component.ts
import { CommonModule } from '@angular/common';
import { Component, Input, forwardRef, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, Validator, AbstractControl, ValidationErrors, NgControl } from '@angular/forms';

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
    }
  ]
})
export class InputComponent implements ControlValueAccessor {
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() type: string = 'text';
  @Input() invalid: boolean = false;
  @Input() invalidMessage: string = "";

  value: string = '';
  disabled = false;

  onChange = (value: any) => {};
  onTouched = () => {};



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
    this.value = input.value;
    this.onChange(this.value);
    this.onTouched();
  }
}