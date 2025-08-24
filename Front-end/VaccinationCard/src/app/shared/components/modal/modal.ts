import { NgClass, NgStyle } from '@angular/common';
import { Component, Input } from '@angular/core';

export type ModalSize = 'small' | 'medium' | 'large' | 'extra-large';

@Component({
  selector: 'app-modal',
  imports: [NgClass],
  standalone: true,
  templateUrl: './modal.html',
  styleUrl: './modal.scss'
})
export class Modal {
  @Input() title: string = '';
  @Input() size: ModalSize = 'medium';

  get modalSizeClass(): string {
    return `modal-${this.size}`;
  }
}
