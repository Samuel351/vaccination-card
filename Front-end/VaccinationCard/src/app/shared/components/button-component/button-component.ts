// button.component.ts
import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

export type ButtonVariant = 'primary' | 'secondary' | 'success' | 'warning' | 'danger' | 'ghost' | 'outline';
export type ButtonSize = 'small' | 'medium' | 'large';
export type ButtonType = 'button' | 'submit' | 'reset';

@Component({
  selector: 'app-button',
  templateUrl: './button-component.html',
  imports: [CommonModule],
  styleUrls: ['./button-component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ButtonComponent {
  @Input() text: string = '';
  @Input() variant: ButtonVariant = 'primary';
  @Input() size: ButtonSize = 'medium';
  @Input() type: ButtonType = 'button';
  @Input() disabled: boolean = false;
  @Input() loading: boolean = false;
  @Input() fullWidth: boolean = false;
  @Input() icon: string = '';
  @Input() iconPosition: 'left' | 'right' = 'left';
  @Input() ariaLabel: string = '';
  @Input() title: string = '';

  @Output() clicked = new EventEmitter<Event>();

  onClick(event: Event): void {
    if (!this.disabled && !this.loading) {
      this.clicked.emit(event);
    }
  }

  get buttonClasses(): string {
    const classes = [
      'btn',
      `btn-${this.variant}`,
      `btn-${this.size}`,
      this.fullWidth ? 'btn-full-width' : '',
      this.disabled ? 'btn-disabled' : '',
      this.loading ? 'btn-loading' : '',
      this.icon && !this.text ? 'btn-icon-only' : ''
    ];

    return classes.filter(Boolean).join(' ');
  }

  get isDisabled(): boolean {
    return this.disabled || this.loading;
  }

  get displayText(): string {
    return this.loading ? 'Carregando...' : this.text;
  }

  get ariaLabelText(): string {
    if (this.ariaLabel) return this.ariaLabel;
    if (this.loading) return 'Carregando';
    return this.text || 'Botão';
  }
}
