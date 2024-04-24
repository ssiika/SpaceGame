import { Component, ChangeDetectorRef, Input } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate,
  transition,
} from '@angular/animations';

@Component({
  selector: 'app-save-popup',
  templateUrl: './save-popup.component.html',
  styleUrls: ['./save-popup.component.css'],
  animations: [
    trigger('popup', [
      state('open', style({    
        opacity: 1
      })),
      state('closed', style({
        top: '-100px',
        opacity: 0
      })),
      transition('open => closed', [
        animate('1s')
      ]),
      transition('closed => open', [
        animate('0.5s')
      ]),
    ]),
  ],
})
export class SavePopupComponent {

  constructor(private cdr: ChangeDetectorRef) { }

  @Input() saveSuccess: boolean = false;
  @Input() message: string = '';

  isOpen: boolean = false;

  ngAfterViewInit(): void {
    this.isOpen = true;
    this.cdr.detectChanges();

    setTimeout(() => {
      this.isOpen = false;
    }, 2500);
  };
}
