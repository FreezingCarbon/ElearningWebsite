import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class StudentAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) {}
  canActivate(): boolean {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      if (this.authService.loggedIn() && data['role'] === 'Student') {
        return true;
      }
    }
    this.alertify.error('You shall not pass !!!');
    this.router.navigate['/home'];
    return false;
  }
}
