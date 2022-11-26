import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { CacheItem } from "../interfaces/cache.interfaces";


@Injectable({
    providedIn: 'root'
})
export class CacheService{
    private data: CacheItem[] = [];


    public set(cacheItem: CacheItem){
        this.remove(cacheItem.key);
        this.data.push(cacheItem);
    }

    public get<T>(key: string): Observable<T>{
        let item = this.data.find(data => data.key === key);
        if(item){
            return of(item.value);
        }
        return null;
    }

    public remove(key: string){
        this.data = this.data.filter(data => data.key != key);
    }
}