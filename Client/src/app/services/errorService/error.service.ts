import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ServiceResponse } from '../../types';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor() { }

  handleError<T>() {
    return (error: any): Observable<ServiceResponse<T>> => {

      const errorResponse: ServiceResponse<T> = {
        success: false,
        message: (error.error && error.error.message)
          ? error.error.message
          : error.message
      }

      console.log(errorResponse.message); // log to console 

      // Let the app keep running by returning an empty result.
      return of(errorResponse);
    };
  }
}
