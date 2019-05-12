import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { StudentService } from '../_services/student.service';

@Injectable()
export class EnrolledResolver implements Resolve<Object> {
    constructor(private studentService: StudentService, private router: Router, private authService: AuthService,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Object> {
        return this.studentService.isEnrolled(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Problem retriving your data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
