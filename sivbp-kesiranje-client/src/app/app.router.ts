import {  Routes } from "@angular/router";
import { PostPageComponent } from "./post/post-page.component";
import { UserEfficiencyComponent } from "./user-efficiency/user-efficiency.component";
import { UserPageComponent } from "./user-page/user-page.component";


export const router: Routes = [
    {
        path: '',
        component: UserPageComponent,
        pathMatch: 'full'
    },
    {
        path: 'efficiency',
        component: UserEfficiencyComponent
    },
    {
        path: 'posts/:id',
        component: PostPageComponent
    }
];