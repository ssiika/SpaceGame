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

  message: string = '';
  saveFile?: SaveFile;
  username: string = '';
  isLoading: boolean = false;

  ngOnInit(): void {
    this.username = this.authService.getValidUsername();

    if (this.username) {
      this.isLoading = true;

      this.saveService.getSaveFile()
        .subscribe(res => {
          if (!res.success) {
            this.message = res.message;
          } else {
            this.saveFile = res.data;
          }
          this.isLoading = false;
        }); 
    }
  };
}
