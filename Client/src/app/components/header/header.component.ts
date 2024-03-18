import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  isLoggedIn: boolean = false;
  openLogin: boolean = false;

  toggleLoginBox(): void {
    this.openLogin = !this.openLogin;
  }

  ngOnInit(): void {
    console.log(this.openLogin);
  }
}
