import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }

login(model: any) {
  if(model.isTeacher === true) {
    return this.http.post(this.baseUrl + 'teacher/login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if(user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  } else {
    return this.http.post(this.baseUrl + 'student/login', model)
    .pipe(
      map((response: any) => {
        const user = response;
        if(user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }
}

}