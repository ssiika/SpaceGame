import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(
    private formBuilder: FormBuilder
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
  }
}
