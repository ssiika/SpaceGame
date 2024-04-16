import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authService/auth.service';
import { SaveService } from '../../services/saveService/save.service';
import { SaveFile } from '../../types';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(
    private authService: AuthService,
    private saveService: SaveService,
    private router: Router
  ) { }

  isLoggedIn: boolean = false;
  openLogin: boolean = false;

  popupIsOpen: boolean = false;
  saveSuccess: boolean = false;
  popupMessage: string = '';
  timer?: ReturnType<typeof setTimeout>;

  username: string = '';

  toggleLoginBox(): void {
    this.openLogin = !this.openLogin;
  }

  openSavePopup(message: string): void {

    // Clear timeout and close current popup is save is pressed multiple times in succession
    if (this.popupIsOpen) {
      if (this.timer) {
        clearTimeout(this.timer);
      }
      this.popupIsOpen = false;
    }

    this.popupIsOpen = true;
    this.popupMessage = message;
    this.timer = setTimeout(() => {
      this.popupIsOpen = false;
    }, 5000)
  }

  save(): void {
    var cacheSave = this.saveService.getCacheSave();
    if (!cacheSave) {
      console.log('No file to save');
      this.saveSuccess = false;
      this.openSavePopup('No file to save');
      return;
    }

    this.saveService.updateSaveFile(cacheSave)
      .subscribe(res => {
        if (!res.success) {
          console.log(res.message);
          this.saveSuccess = false;
          this.openSavePopup(res.message);
        } else {
          console.log(res.data);
          this.saveSuccess = true;
          this.openSavePopup('Successfully saved game')
        }
      });
  }

  logout(): void {
    localStorage.removeItem('user');
    this.router.navigate(['/reload']);
  }

  ngOnInit(): void {
    this.username = this.authService.getValidUsername();

    if (this.username) {
      this.isLoggedIn = true;
    }
  };
}
