import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../../services/authService/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
  ) { }

  message: string = '';
  isLoading: boolean = false;  

  loginForm = this.formBuilder.group({
    username: '',
    password: ''
  });

  onSubmit(): void {
    const userData = {
      username: this.loginForm.value.username!?.trim(),
      password: this.loginForm.value.password!?.trim(),
    }

    if (!userData.username || !userData.password) {
      this.message = 'Please provide a username and password'
      return
    }

    this.isLoading = true;

    this.authService.login(userData)
      .subscribe(res => {
        if (!res.success) {
          this.message = res.message;
        } else {
          localStorage.setItem('user', JSON.stringify(res.data))
        }
        this.isLoading = false;
      });
    this.loginForm.reset();
  }
}
