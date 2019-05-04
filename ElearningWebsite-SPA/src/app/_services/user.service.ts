import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { Teacher } from '../_models/teacher';
import { Course } from '../_models/course';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getCourses(page?, itemsPerPage?): Observable<PaginatedResult<Course[]>> {
    const paginatedResult: PaginatedResult<Course[]> = new PaginatedResult<Course[]>();
    let params = new HttpParams();


    if(page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Course[]>(this.baseUrl + 'courses', { observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if(response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }
          return paginatedResult;
        })
      );
  }

  getCourse(id: number) {
    return this.http.get<Course>(this.baseUrl + 'courses/' + id);
  }
}
