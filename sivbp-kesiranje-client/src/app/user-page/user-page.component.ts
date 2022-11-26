import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IPageInfo, VirtualScrollerComponent } from 'ngx-virtual-scroller';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { PaginatedList } from '../interfaces/pagination.interfaces';
import { User } from '../interfaces/user.interface';
import { DecoratedUserService } from '../services/decorated-user.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit, OnDestroy {
  hasNextPage: boolean;
  currentPage: number = 1;
  itemsPerPage: number = 20;
  users: User[] = [];

  @ViewChild('scroll') scroller: VirtualScrollerComponent;

  private readonly destroySubject$ = new Subject();

  constructor(private userService: DecoratedUserService,
    private router: Router) { }

  ngOnInit() {
    this.loadUserData();
  }

  loadUserData(): void{
    this.userService.getPaginatedUsers({
      pageNumber: this.currentPage,
      pageSize: this.itemsPerPage
    }).pipe(takeUntil(this.destroySubject$))
    .subscribe((data: PaginatedList<User>) => {
      this.users.push(...data.items);
      this.hasNextPage = data.hasNextPage;
      this.currentPage ++;
    })
  }

  scrolledToEnd(scrollDownEvent: IPageInfo): void{
    if(scrollDownEvent.endIndex == this.users.length - 1 && scrollDownEvent.endIndex != -1){
      this.loadUserData();
    }
  }

  getRandomUserPost(posts: number[]){
    let postId = posts[Math.floor(Math.random()*posts.length)];
    this.router.navigateByUrl(`/posts/${postId}`);
  }

  ngOnDestroy(): void{
    this.destroySubject$.next();
    this.destroySubject$.complete();
  }
}
