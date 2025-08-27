import { CommonModule } from '@angular/common';
import { Component, ElementRef, Input, OnDestroy, ViewChild, forwardRef, OnInit, OnChanges } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

export interface Option {
  name: string,
  value: any,
  disabled: boolean
}

@Component({
  selector: 'app-dropdown',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './dropdown.html',
  styleUrl: './dropdown.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => Dropdown),
      multi: true
    }
  ]
})
export class Dropdown implements OnInit, OnDestroy, OnChanges, ControlValueAccessor {
  @Input() options: Option[] = [];
  @Input() placeholder = 'Selecione uma opção';
  @Input() label = '';
  @Input() disabled = false;

  @ViewChild('trigger') triggerRef!: ElementRef;
  @ViewChild('menu') menuRef!: ElementRef;

  isOpen = false;
  selectedOption: Option | null = null;
  value: any = null;

  private clickListener = this.onDocumentClick.bind(this);

  // ControlValueAccessor callbacks
  private onChange = (value: any) => {};
  private onTouched = () => {};

  ngOnInit() {
    this.updateSelectedOption();
    document.addEventListener('click', this.clickListener);
  }

  ngOnDestroy() {
    document.removeEventListener('click', this.clickListener);
  }

  ngOnChanges() {
    this.updateSelectedOption();
  }

  // ControlValueAccessor implementation
  writeValue(value: any): void {
    this.value = value;
    this.updateSelectedOption();
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  toggle() {
    if (this.disabled) return;
    
    if (!this.isOpen) {
      this.onTouched(); // Mark as touched when opening
    }
    
    this.isOpen = !this.isOpen;
  }

  selectOption(option: Option) {
    if (option.disabled) return;
    
    this.value = option.value;
    this.selectedOption = option;
    this.onChange(option.value); // Notify form control of value change
    this.isOpen = false;
  }

  private updateSelectedOption() {
    this.selectedOption = this.options.find(opt => opt.value === this.value) || null;
  }

  private onDocumentClick(event: Event) {
    const target = event.target as HTMLElement;
    const dropdown = this.triggerRef?.nativeElement;
    const menu = this.menuRef?.nativeElement;
    
    if (dropdown && !dropdown.contains(target) && 
        (!menu || !menu.contains(target))) {
      if (this.isOpen) {
        this.onTouched(); // Mark as touched when closing via outside click
      }
      this.isOpen = false;
    }
  }
}