import { Component } from '@angular/core';
import { SaveFile, GetUserDto } from '../../types';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

  saveFile?: SaveFile;
}
