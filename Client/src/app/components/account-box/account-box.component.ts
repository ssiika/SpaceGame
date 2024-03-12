import { Component } from '@angular/core';

@Component({
  selector: 'app-account-box',
  templateUrl: './account-box.component.html',
  styleUrls: ['./account-box.component.css']
})
export class AccountBoxComponent {
  openLogin: boolean = true;

  changePage(): void {
    this.openLogin = !this.openLogin;
  }
}
