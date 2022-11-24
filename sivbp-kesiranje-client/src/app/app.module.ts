import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';

import { AppComponent } from './app.component';
import { router } from './app.router';
import { PostPageComponent } from './post/post-page.component';
import { UserEfficiencyComponent } from './user-efficiency/user-efficiency.component';
import { UserPageComponent } from './user-page/user-page.component';

@NgModule({
  declarations: [AppComponent, UserPageComponent, UserEfficiencyComponent, PostPageComponent],
  imports: [BrowserModule, RouterModule.forRoot(router), VirtualScrollerModule, HttpClientModule, CommonModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
