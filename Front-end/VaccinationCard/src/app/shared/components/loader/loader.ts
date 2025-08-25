import { CommonModule, NgStyle } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loader',
  imports: [NgStyle, CommonModule],
  standalone: true,
  templateUrl: './loader.html',
  styleUrl: './loader.scss'
})
export class Loader {
  @Input() message: string = 'Carregando...'; // mensagem opcional
}
