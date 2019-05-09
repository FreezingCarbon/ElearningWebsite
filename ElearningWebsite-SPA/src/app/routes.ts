import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './course/courses/courses.component';
import { AboutComponent } from './about/about.component';
import { CoursesResolver } from './_resolvers/courses.resolver';
import { CourseNDetailComponent } from './course/course-n-detail/course-n-detail.component';
import { CourseNResolver } from './_resolvers/course-n.resolver';
import { TCoursesResolver } from './_resolvers/t-courses.resolver';
import { TeacherAuthGuard } from './_guard/teacher-auth.guard';
import { CourseEditComponent } from './course/course-edit/course-edit.component';
import { EditCourseResolver } from './_resolvers/edit-course.resolver';
import { PreventUsavedChanges } from './_guard/prevent-unsaved-changes.guard';
import { CourseCreateComponent } from './course/course-create/course-create.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: 'courses', component: CoursesComponent, resolve: {courses: CoursesResolver} },
            { path: 'courses/:id', component: CourseNDetailComponent, resolve: {course: CourseNResolver} }
        ]
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [TeacherAuthGuard],
        children: [
            { path: 'teacher/courses', component: CoursesComponent, resolve: {courses: TCoursesResolver} },
            { path: 'teacher/courses/edit/:id/:editMode', component: CourseEditComponent,
                canDeactivate: [PreventUsavedChanges] , resolve: { course: EditCourseResolver } },
            { path: 'teacher/courses/:id', component: CourseNDetailComponent, resolve: {course: EditCourseResolver} },
            { path: 'teacher/course/create', component: CourseCreateComponent }
        ]
    },
    { path: 'student/register/:registerMode/:isTeacher', component: HomeComponent},
    { path: 'about', component: AboutComponent},
    { path: '**', redirectTo: '', pathMatch: 'full' },
];
