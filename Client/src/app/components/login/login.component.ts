import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private formBuilder: FormBuilder
  ) { }

  message: string = '';
  isLoading: boolean = false;

  loginForm = this.formBuilder.group({
    username: '',
    password: ''
  });

  onClick(): void {
    console.log('Click handler not implemented yet')
  }

  onSubmit(): void {
    const userData = {
      username: this.loginForm.value.username!?.trim(),
      password: this.loginForm.value.password!?.trim(),
    }

    if (!userData.username || !userData.password) {
      this.message = 'Please provide a username and password'
      return
    }
  }
}
