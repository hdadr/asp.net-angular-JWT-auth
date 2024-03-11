import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const BASE_URL = '/api';
const TEST_CONTROLLER_URL = BASE_URL + '/test';
const UNAUTHORIZED_URL = TEST_CONTROLLER_URL + '/unauthorized';
const USERS_URL = TEST_CONTROLLER_URL + '/users';
const ADMINS_URL = TEST_CONTROLLER_URL + '/admins';

@Injectable()
export class TestService {

  constructor(private http: HttpClient) {}

  public getUnauthorizedDataLoggedInUser(): Observable<{unauthorizedData: string[]}> {
    return this.http.get<{unauthorizedData: string[]}>(UNAUTHORIZED_URL);
  }

  public getDataForUserRole(): Observable<{userRoleData: string[]}> {
    return this.http.get<{userRoleData: string[]}>(USERS_URL);
  }

  public getDataForAdminRole(): Observable<{adminRoleData: string[]}> {
    return this.http.get<{adminRoleData: string[]}>(ADMINS_URL);
  }
}
