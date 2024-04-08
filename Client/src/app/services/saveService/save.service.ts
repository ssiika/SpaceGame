import { Injectable } from '@angular/core';
import { SaveFile, ServiceResponse, UpdateSaveFileDto } from '../../types';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorService } from '../errorService/error.service';
import { AuthService } from '../authService/auth.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

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

  cacheSaveFile?: UpdateSaveFileDto;

  getCacheSave(): UpdateSaveFileDto | null {
    return this.cacheSaveFile ? this.cacheSaveFile : null;
  }

  setCacheSave(saveFile: UpdateSaveFileDto): void {
    this.cacheSaveFile = saveFile;
  }

  getSaveFile(): Observable<ServiceResponse<SaveFile>> {
    const options = this.initOptions();
    return this.http.get<ServiceResponse<SaveFile>>(this.API_URL, options)
      .pipe(
        catchError(this.errorService.handleError<SaveFile>())
      );
  }

  updateSaveFile(saveFile: UpdateSaveFileDto): Observable<ServiceResponse<SaveFile>> {
    const options = this.initOptions();
    return this.http.put<ServiceResponse<SaveFile>>(this.API_URL, saveFile, options)
      .pipe(
        catchError(this.errorService.handleError<SaveFile>())
      );
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
