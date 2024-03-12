import { Component } from '@angular/core';

@Component({
  selector: 'app-login-loading',
  templateUrl: './login-loading.component.html',
  styleUrls: ['./login-loading.component.css']
})
export class LoginLoadingComponent {
  isLoginPage: boolean = true;

  changePage(): void {
    this.isLoginPage = !this.isLoginPage;
  }
}
