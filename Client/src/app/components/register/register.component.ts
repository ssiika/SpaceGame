import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../../services/authService/auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  message: string = '';
  isLoading: boolean = false;

  registerForm = this.formBuilder.group({
    username: '',
    password: '',
    passwordConfirm: ''
  });

  onSubmit(): void {
    if (!this.registerForm.value.username ||
      !this.registerForm.value.password ||
      !this.registerForm.value.passwordConfirm
    ) {
      this.message = 'Please provide an input for all fields'
      return
    }

    if (this.registerForm.value.password !== this.registerForm.value.passwordConfirm) {
      this.message = 'Passwords do not match'
      return;
    }

    const userData = {
      username: this.registerForm.value.username!?.trim(),
      password: this.registerForm.value.password!?.trim(),
    }

    this.isLoading = true;

    this.authService.register(userData)
      .subscribe(res => {
        if (!res.success) {
          this.message = res.message;
        } else {
          localStorage.setItem('user', JSON.stringify(res.data));
          this.router.navigate(['/reload']);
        }
        this.isLoading = false;
      });
    this.registerForm.reset();
  }
}
