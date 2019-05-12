import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { Course } from '../_models/course';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {
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
      return this.http.get<Course[]>(this.baseUrl + 'teacher/' + data['nameid'] + '/courses', { observe: 'response', params})
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

  getCourse(id: number) {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.http.get(this.baseUrl + 'teacher/' + data['nameid'] + '/courses/' + id);
    }
  }

  updateCourse(teacherId: number, courseId: number, course: Course) {
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.put(this.baseUrl + 'teacher/' + teacherId + '/courses/' + courseId, course, {headers: header});
  }

  deleteVideo(courseId: number, videoId: number) {
    const data = JSON.parse(localStorage.getItem('user'));
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });

    if (data) {
      return this.http.delete(this.baseUrl + 'teacher/' + data['nameid'] + '/courses/' +
        courseId + '/videos/' + videoId, {headers: header});
    }
  }

  addCourse(model: any) {
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      return this.http.post(this.baseUrl + 'teacher/' + data['nameid'] + '/courses/',  model, {headers: header});
    }
  }

  deleteCourse(courseId) {
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    });
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      return this.http.delete(this.baseUrl + 'teacher/' + data['nameid'] + '/courses/' + courseId, {headers: header});
    }
  }
}
