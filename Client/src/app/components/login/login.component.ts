import { Component } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router
  ) { }

  message: string = '';
  isLoading: boolean = false;  

  loginForm = this.formBuilder.group({
    username: '',
    password: ''
  });

  reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
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

    this.isLoading = true;

    this.authService.login(userData)
      .subscribe(res => {
        if (!res.success) {
          this.message = res.message;
        } else {
          localStorage.setItem('user', JSON.stringify(res.data));
          this.router.navigate(['/reload']);
        }
        this.isLoading = false;
      });
    this.loginForm.reset();

  }

  ngOnInit(): void {
    if (this.authService.getValidUsername()) {
      this.router.navigate(['/'])
    };
  }
}
