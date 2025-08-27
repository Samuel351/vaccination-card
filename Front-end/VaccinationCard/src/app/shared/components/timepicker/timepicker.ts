import { Component, EventEmitter, forwardRef, Input, Optional, Output, Self } from '@angular/core';
import { NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';

export interface TimeValue {
  hours: number;
  minutes: number;
}

@Component({
  selector: 'app-timepicker',
  imports: [],
  templateUrl: './timepicker.html',
  styleUrl: './timepicker.scss',
  providers: []
})
export class Timepicker {
  @Input() disabled: boolean = false;
  @Input() label?: string = undefined;
  value: string = '';

  @Output() seletectedHour = new EventEmitter<string>();

  // Inject NgControl to access parent form control state & errors
  constructor(@Optional() @Self() public ngControl: NgControl) {
    if (this.ngControl) {
      this.ngControl.valueAccessor = this;
    }
  }

  private onChange: (value: string) => void = () => {};
  private onTouched: () => void = () => {};

  writeValue(value: string): void {
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

  onTimeChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.value = input.value;
    this.seletectedHour.emit(this.value);
    this.onChange(this.value);
    this.markTouched();
  }

  markTouched() {
    this.onTouched();
  }

  // Helper to check if control is invalid & touched
  get invalid(): boolean {
    return !!this.ngControl?.control?.invalid && !!this.ngControl?.control?.touched;
  }

  // Returns error messages based on validation errors
  get errorMessage(): string | null {
    const errors = this.ngControl?.control?.errors;
    if (!errors) return null;

    if (errors['required']) return 'This field is required';
    if (errors['min']) return `Time must be after ${errors['min'].min}`;
    if (errors['max']) return `Time must be before ${errors['max'].max}`;

    return 'Invalid value';
  }
}


