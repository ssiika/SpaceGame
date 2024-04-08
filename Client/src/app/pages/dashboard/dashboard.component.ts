import { Component } from '@angular/core';
import { SaveFile, UpdateSaveFileDto } from '../../types';
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

  // This section is only for testing update save file functionality
  dummySeed: number = 1;
  dummyStage: number = 2;
  dummyDistance: number = 3;

  dummySet(): void {

    this.saveService.setCacheSave({
      seed: this.dummySeed,
      stage: this.dummyStage,
      distance: this.dummyDistance
    });
  }

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
