import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedList, PaginatedRequest } from '../interfaces/pagination.interfaces';
import { User } from '../interfaces/user.interface';
import { CacheService } from './cache.service';
import { UserService } from './user.service';

@Injectable({
    providedIn: 'root',
})
export class DecoratedUserService{
    private readonly userBaseUrl = `${environment.baseUrl}/users`;

    constructor(private cacheService: CacheService, private userService: UserService) {  }

    public getPaginatedUsers(request: PaginatedRequest): Observable<PaginatedList<User>> {
        let key = `${this.userBaseUrl}?pageNumber=${request.pageNumber}&pageSize=${request.pageSize}`;
        let cachedValue = this.cacheService.get<PaginatedList<User>>(key);
        if(cachedValue == null){
            return this.userService.getPaginatedUsers(request).pipe(
                tap((data: PaginatedList<User>) => {
                    this.cacheService.set({
                        key: key,
                        value: data
                    })
                })
            );
        }
        return cachedValue;
    }
}
