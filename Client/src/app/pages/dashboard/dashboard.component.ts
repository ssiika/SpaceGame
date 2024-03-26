import { Component } from '@angular/core';
import { SaveFile } from '../../types';
import { AuthService } from '../../services/authService/auth.service';
import { SaveService } from '../../services/saveService/save.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(
    private authService: AuthService,
    private saveService: SaveService
  ) { }

  saveFile?: SaveFile;
  username: string = '';

  ngOnInit(): void {
    this.username = this.authService.getValidUsername();

    if (this.username) {
      this.saveService.getSaveFile();
    }
  };
}
