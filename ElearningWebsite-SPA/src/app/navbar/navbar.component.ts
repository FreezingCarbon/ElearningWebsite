import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = { };
  currentUser: any;
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      this.currentUser = data['unique_name'];
    }
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/home']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  isStudent() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      if(this.loggedIn() && data['role'] === 'Student') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      if(this.loggedIn() && data['role'] === 'Teacher') {
        return true;
      } else {
        return false;
      }
    }
    return false;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.router.navigate(['/home']);
    this.alertify.message('Logged out');
  }

}
