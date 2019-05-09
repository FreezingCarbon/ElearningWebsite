import { Component, OnInit } from '@angular/core';
import { Course } from '../../_models/course';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: Course[];
  pagination: Pagination;

  constructor(private userService: UserService, private alertify: AlertifyService,
    private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.courses = data['courses'].result;
      this.pagination = data['courses'].pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadCourses();
  }

  loadCourses() {
    this.userService.getCourses(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe((res: PaginatedResult<Course[]>) => {
      this.courses = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      return data['role'] === 'Teacher';
    } else {
      return false;
    }
  }

  addCourse() {
    this.router.navigate(['teacher/course/create']);
  }
}
