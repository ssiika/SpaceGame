import { Injectable } from '@angular/core';
import { SaveFile, ServiceResponse } from '../../types';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorService } from '../errorService/error.service';
import { AuthService } from '../authService/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SaveService {
  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private errorService: ErrorService
  ) { }

  API_URL: string = '/api/SaveFile/';

  getSaveFile(): void {
    const options = this.initOptions();
    // Implement get request
  }

  private initOptions(): Object {
    const token = this.authService.getToken();

    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      })
    };
  }
}
