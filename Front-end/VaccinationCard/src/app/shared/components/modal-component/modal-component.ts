// modal.component.ts
import { CommonModule, NgStyle } from '@angular/common';
import { 
  Component, 
  Input, 
  Output, 
  EventEmitter, 
  HostListener,
  ElementRef,
  ChangeDetectionStrategy,
  ViewEncapsulation
} from '@angular/core';

export type ModalSize = 'small' | 'medium' | 'large' | 'extra-large' | 'full';
export type ModalPosition = 'center' | 'top' | 'bottom';

@Component({
  selector: 'app-modal',
  templateUrl: './modal-component.html',
  styleUrls: ['./modal-component.scss'],
  standalone: true,
  imports: [NgStyle, CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class ModalComponent {
  @Input() size: ModalSize = 'medium';
  @Input() position: ModalPosition = 'center';
  @Input() closable: boolean = true;
  @Input() closeOnBackdropClick: boolean = true;
  @Input() closeOnEscape: boolean = true;
  @Input() showHeader: boolean = true;
  @Input() showFooter: boolean = false;
  @Input() title: string = '';
  @Input() headerClass: string = '';
  @Input() bodyClass: string = '';
  @Input() footerClass: string = '';
  @Input() overlayClass: string = '';
  @Input() modalClass: string = '';
  @Input() zIndex: number = 1050;
  @Input() animationDuration: number = 300;
  @Input() customWidth: string = '';
  @Input() customHeight: string = '';
  @Input() maxWidth: string = '';
  @Input() maxHeight: string = '';
  @Input() padding: string = '';
  @Input() borderRadius: string = '';

  @Output() close = new EventEmitter<void>();
  @Output() backdropClick = new EventEmitter<void>();
  @Output() escapePress = new EventEmitter<void>();

  constructor(private elementRef: ElementRef) {}

  onBackdropClick(event: Event): void {
    if (event.target === event.currentTarget) {
      this.backdropClick.emit();
      if (this.closeOnBackdropClick && this.closable) {
        this.close.emit();
      }
    }
  }

  onCloseClick(): void {
    if (this.closable) {
      this.close.emit();
    }
  }


  get modalStyles(): { [key: string]: any } {
    const styles: { [key: string]: any } = {
      'z-index': this.zIndex,
      'animation-duration': `${this.animationDuration}ms`
    };

    if (this.customWidth) styles['width'] = this.customWidth;
    if (this.customHeight) styles['height'] = this.customHeight;
    if (this.maxWidth) styles['max-width'] = this.maxWidth;
    if (this.maxHeight) styles['max-height'] = this.maxHeight;
    if (this.borderRadius) styles['border-radius'] = this.borderRadius;

    return styles;
  }

  get bodyStyles(): { [key: string]: any } {
    const styles: { [key: string]: any } = {};
    
    if (this.padding) styles['padding'] = this.padding;
    
    return styles;
  }

  get modalClasses(): string {
    const classes = [
      'modal',
      `modal-${this.size}`,
      `modal-${this.position}`,
      this.modalClass
    ];

    return classes.filter(Boolean).join(' ');
  }

  get overlayClasses(): string {
    const classes = [
      'modal-overlay',
      this.overlayClass
    ];

    return classes.filter(Boolean).join(' ');
  }

  get headerClasses(): string {
    const classes = [
      'modal-header',
      this.headerClass
    ];

    return classes.filter(Boolean).join(' ');
  }

  get bodyClasses(): string {
    const classes = [
      'modal-body',
      this.bodyClass
    ];

    return classes.filter(Boolean).join(' ');
  }

  get footerClasses(): string {
    const classes = [
      'modal-footer',
      this.footerClass
    ];

    return classes.filter(Boolean).join(' ');
  }
}
