import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { Course } from '../_models/course';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient, private authService: AuthService) { }

  getCourses(page?, itemsPerPage?): Observable<PaginatedResult<Course[]>> {
    const paginatedResult: PaginatedResult<Course[]> = new PaginatedResult<Course[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      });

      return this.http.get<Course[]>(this.baseUrl + 'student/' + data['nameid'] + '/courses',
      { observe: 'response', headers: header, params} )
        .pipe(
          map(response => {
            paginatedResult.result = response.body;
            if (response.headers.get('Pagination') != null) {
              paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
            }
            return paginatedResult;
          })
        );
    }
  }

  isEnrolled(courseId: number): Observable<Object> {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      });
    return this.http.get(this.baseUrl + 'student/' + data['nameid'] + '/courses/' + courseId, { headers: header });
    }
  }

  enroll(courseId: number) {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      const header = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      });
      return this.http.post(this.baseUrl + 'student/' + data['nameid'] + '/courses/' + courseId, {headers: header});
    }
  }

}
