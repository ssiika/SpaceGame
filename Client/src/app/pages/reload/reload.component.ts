import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reload',
  templateUrl: './reload.component.html',
  styleUrls: ['./reload.component.css']
})
export class ReloadComponent {

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
    this.router.navigate(['/'])
  }
}
