import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedList, PaginatedRequest } from '../interfaces/pagination.interfaces';
import { User, UserEfficiency } from '../interfaces/user.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private readonly userBaseUrl = `${environment.baseUrl}/users`;
  constructor(private httpClient: HttpClient) {}

  public getPaginatedUsers(request: PaginatedRequest): Observable<PaginatedList<User>> {
    return this.httpClient.get<PaginatedList<User>>(`${this.userBaseUrl}?pageNumber=${request.pageNumber}&pageSize=${request.pageSize}`);
  }

  public getUserEfficiency(): Observable<UserEfficiency[]>{
    return this.httpClient.get<UserEfficiency[]>(`${this.userBaseUrl}/efficiency`);
  }
}
