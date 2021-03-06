import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { Course } from '../_models/course';
import { AlertifyService } from '../_services/alertify.service';
import { TeacherService } from '../_services/teacher.service';

@Injectable()
export class TCoursesResolver implements Resolve<Course[]> {
    constructor(private teacherService: TeacherService, private router: Router, private authService: AuthService,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Course[]> {
        return this.teacherService.getCourses().pipe(
            catchError(error => {
                this.alertify.error('Problem retriving your data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
