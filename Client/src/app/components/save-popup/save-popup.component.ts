import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-save-popup',
  templateUrl: './save-popup.component.html',
  styleUrls: ['./save-popup.component.css']
})
export class SavePopupComponent {
  @Input() saveSuccess: boolean = false;
  @Input() message: string = '';
}
