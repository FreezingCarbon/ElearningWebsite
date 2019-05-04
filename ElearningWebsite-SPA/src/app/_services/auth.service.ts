import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { DecodedToken } from '../_models/decoded-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';

  jwtHelper = new JwtHelperService();
  decodedToken: DecodedToken;
  currentUser: any;

  constructor(private http: HttpClient) { }

  login(model: any) {
    if (model.isTeacher === true) {
      return this.http.post(this.baseUrl + 'teacher/login', model)
        .pipe(
          map((response: any) => {
            const user = response;
            if (user) {
              localStorage.setItem('token', user.token);
              this.decodedToken = this.jwtHelper.decodeToken(user.token);
              localStorage.setItem('user', JSON.stringify(this.decodedToken));
              this.currentUser = user.user;
            }
          })
        );
    } else {
      return this.http.post(this.baseUrl + 'student/login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            localStorage.setItem('user', JSON.stringify(this.decodedToken));
            this.currentUser = user.user;
            console.log(this.currentUser);
          }
        })
      );
    }
  }

  loggedIn() {
    const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
  }

  register(user: any, isTeacher: boolean) {
    if (isTeacher === true) {
      return this.http.post(this.baseUrl + 'teacher/register', user);
    } else {
      return this.http.post(this.baseUrl + 'student/register', user);
    }
  }
}
