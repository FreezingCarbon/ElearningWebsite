import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './courses/courses.component';
import { AboutComponent } from './about/about.component';
import { CourseslistComponent } from './courseslist/courseslist.component';
import { AuthGuard } from './_guard/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'courses', component: CoursesComponent},
    { path: 'courseslist', component: CourseslistComponent, canActivate: [AuthGuard]},
    { path: 'about', component: AboutComponent},
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
