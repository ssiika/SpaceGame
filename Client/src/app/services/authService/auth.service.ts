import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserData, ServiceResponse, UserCreds } from '../../types';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorService } from '../../services/errorService/error.service'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private errorService: ErrorService
  ) { }

  API_URL: string = '/api/Users/';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  login(userData: UserData): Observable<ServiceResponse<UserCreds>> {
    return this.http.post<ServiceResponse<UserCreds>>(this.API_URL + 'login', userData, this.httpOptions)
      .pipe(
        catchError(this.errorService.handleError<UserCreds>())
      );
  }

  test(): Observable<any> {
    return this.http.get<any>('weatherforecast', this.httpOptions)
      .pipe(
        catchError(this.errorService.handleError<any>())
      );
  }
}
