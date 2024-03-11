import { Injectable } from '@angular/core';
import { RefreshToken } from 'src/app/models/refresh-token.model';

export interface AccessTokenClaims {
  aud: string;
  exp: number;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string; // ex: "User" or "User, Admin"...
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name': string;
  iss: string;
  nbf: string;
}

const localStorageKeys = {
  accessToken: 'accessToken',
  refreshToken: 'refreshToken',
};

@Injectable()
export class TokenService {
  public saveAuthTokens(accessToken: string, refreshToken: RefreshToken): void {
    localStorage.setItem(localStorageKeys.accessToken, accessToken);
    localStorage.setItem(localStorageKeys.refreshToken, JSON.stringify(refreshToken));
  }

  public updateAccessToken(accessToken: string): void {
    localStorage.setItem(localStorageKeys.accessToken, accessToken);
  }

  public getAccessToken(): string {
    return localStorage.getItem(localStorageKeys.accessToken);
  }

  public getRefreshToken(): RefreshToken {
    return JSON.parse(localStorage.getItem(localStorageKeys.refreshToken)) as RefreshToken;
  }

  public removeAuthTokens(): void {
    localStorage.removeItem(localStorageKeys.accessToken);
    localStorage.removeItem(localStorageKeys.refreshToken);
  }

  public getAccessTokenClaims(accessToken: string): AccessTokenClaims {
    return JSON.parse(atob(accessToken.split('.')[1]));
  }
}
