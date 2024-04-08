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

  username: string = '';

  toggleLoginBox(): void {
    this.openLogin = !this.openLogin;
  }

  save(): void {
    var cacheSave = this.saveService.getCacheSave();
    if (!cacheSave) {
      console.log('No file to save');
      return;
    }

    this.saveService.updateSaveFile(cacheSave)
      .subscribe(res => {
        if (!res.success) {
          console.log(res.message);
        } else {
          console.log(res.data);
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
