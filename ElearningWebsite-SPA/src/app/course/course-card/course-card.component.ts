import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {
  @Input() course: Course;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {

  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn() && data['role'] === 'Teacher';
    } else {
      return false;
    }
  }

  isStudent() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn() && data['role'] === 'Student';
    } else {
      return false;
    }
  }

  toTeacherCourse(courseId) {
    this.router.navigate(['/teacher/courses', courseId]);
  }

  toStudentCourse(courseId) {
    this.router.navigate(['/student/courses', courseId]);
  }
}
