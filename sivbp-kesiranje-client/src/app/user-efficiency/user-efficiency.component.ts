import { Component, OnDestroy, OnInit } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { UserEfficiency } from "../interfaces/user.interface";
import { UserService } from "../services/user.service";

@Component({
  selector: 'app-user-efficiency',
  templateUrl: './user-efficiency.component.html'
})
export class UserEfficiencyComponent implements OnInit, OnDestroy {
  userEfficiencies: Observable<UserEfficiency[]>;
  private readonly destroySubject$ = new Subject();

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userEfficiencies = this.userService.getUserEfficiency();
  }

  ngOnDestroy(): void{
    this.destroySubject$.next();
    this.destroySubject$.complete();
  }
}
