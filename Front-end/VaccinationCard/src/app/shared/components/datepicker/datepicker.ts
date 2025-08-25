import { CommonModule } from '@angular/common';
import { Component, forwardRef, Input, Optional } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatDatepicker, MatDatepickerModule, MatDatepickerToggle, MatDateRangeInput } from '@angular/material/datepicker';
import { MatFormField, MatLabel } from '@angular/material/form-field';

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
export class Datepicker implements ControlValueAccessor {
  @Input() label?: string;
  @Input() datePlaceholder?: string;
  @Input() timePlaceholder?: string;
  @Input() errorMessage?: string;
  @Input() hasError: boolean = false;

  dateValue: string = '';
  timeValue: string = '';
  disabled: boolean = false;

  private onChange = (value: string) => {};
  private onTouched = () => {};

  onDateInput(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.dateValue = target.value;
    this.updateValue();
  }

  onTimeInput(event: Event): void {
    const target = event.target as HTMLInputElement;
    this.timeValue = target.value;
    this.updateValue();
  }

  onBlur(): void {
    this.onTouched();
  }

  private updateValue(): void {
    let combinedValue = '';
    
    if (this.dateValue) {
      combinedValue = this.dateValue;
      if (this.timeValue) {
        combinedValue += `T${this.timeValue}`;
      }
    }
    
    this.onChange(combinedValue);
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
