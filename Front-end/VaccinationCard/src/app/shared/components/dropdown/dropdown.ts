import { CommonModule } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnDestroy, Output, ViewChild } from '@angular/core';

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
  styleUrl: './dropdown.scss'
})
export class Dropdown implements OnDestroy {
  @Input() options: Option[] = [];
  @Input() value: any = null;
  @Input() placeholder = 'Selecione uma opção';
  @Input() label = '';
  @Input() disabled = false;

  @Output() valueChange = new EventEmitter<any>();

  @ViewChild('trigger') triggerRef!: ElementRef;
  @ViewChild('menu') menuRef!: ElementRef;

  isOpen = false;
  selectedOption: Option | null = null;

  private clickListener = this.onDocumentClick.bind(this);

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

  toggle() {
    if (this.disabled) return;
    this.isOpen = !this.isOpen;
  }

  selectOption(option: Option) {
    this.value = option.value;
    this.selectedOption = option;
    this.valueChange.emit(option.value);
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
      this.isOpen = false;
    }
  }
}
