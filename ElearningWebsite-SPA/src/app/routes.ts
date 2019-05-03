import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './course/courses/courses.component';
import { AboutComponent } from './about/about.component';
import { CoursesResolver } from './_resolvers/courses.resolver';
import { CourseNDetailComponent } from './course/course-n-detail/course-n-detail.component';
import { CourseNResolver } from './_resolvers/course-n.resolver';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children:[
            { path: 'courses', component: CoursesComponent, resolve: {courses: CoursesResolver} },
            { path: 'courses/:id', component: CourseNDetailComponent, resolve: {course: CourseNResolver} }
        ]
    },
    { path: 'student/register/:registerMode/:isTeacher', component: HomeComponent},
    { path: 'about', component: AboutComponent},
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
