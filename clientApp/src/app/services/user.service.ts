import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiEndpoints } from '../config/api-endpoints';
import { RegistrationRequest } from '../models/registration.model';

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private apiEndpoints: ApiEndpoints) {}

  public createUser(registrationRequest: RegistrationRequest): Observable<any> { //return value does not matter now
    return this.http.post(this.apiEndpoints.API_USERS, registrationRequest);
  }
}
