import { Injectable } from '@angular/core';

@Injectable()
export class ApiEndpoints {
  private readonly API_ENDPOINT = "/api";

  private readonly API_TOKEN = this.API_ENDPOINT + "/token";
  public readonly API_TOKEN_CREATE = this.API_TOKEN + "/create";
  public readonly API_TOKEN_REVOKE = this.API_TOKEN + "/revoke";
  public readonly API_TOKEN_REFRESH = this.API_TOKEN + "/refresh";

  public readonly API_USERS = this.API_ENDPOINT + "/users";
}
