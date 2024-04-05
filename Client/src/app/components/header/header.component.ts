import { Component } from '@angular/core';
import { AuthService } from '../../services/authService/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  constructor(
    private authService: AuthService
  ) { }

  isLoggedIn: boolean = false;
  openLogin: boolean = false;

  username: string = '';

  toggleLoginBox(): void {
    this.openLogin = !this.openLogin;
  }

  save(): void {
    console.log('saved');
    // Implement save functionality
  }

  logout(): void {
    console.log('logout');
    // Implement logout functionality
  }

  ngOnInit(): void {
    this.username = this.authService.getValidUsername();

    if (this.username) {
      this.isLoggedIn = true;
    }
  };
}
