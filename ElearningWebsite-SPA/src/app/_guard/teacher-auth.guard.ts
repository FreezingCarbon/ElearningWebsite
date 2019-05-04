import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class TeacherAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService) {}

  canActivate(): boolean {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      if (this.authService.loggedIn() && data['role'] === 'Teacher') {
        return true;
      }
    }
    this.alertify.error('You shall not pass !!!');
    // tslint:disable-next-line:no-unused-expression
    this.router.navigate['/home'];
    return false;
  }
}
