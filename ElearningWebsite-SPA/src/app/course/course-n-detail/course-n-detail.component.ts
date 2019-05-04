import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Course } from 'src/app/_models/course';
import { routerNgProbeToken } from '@angular/router/src/router_module';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-course-n-detail',
  templateUrl: './course-n-detail.component.html',
  styleUrls: ['./course-n-detail.component.css']
})
export class CourseNDetailComponent implements OnInit {
  course: Course;
  requirements: string[];
  descriptions: string[];

  constructor(private userService: UserService, private route: ActivatedRoute,
    private alertify: AlertifyService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.course = data['course'];
    });
    this.requirements = this.course.requirement.split('\n');
    this.descriptions = this.course.description.split('\n');
  }

  toRegister() {
    this.router.navigate(['student/register/', true, false]);
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      return this.authService.loggedIn() && data['role'] === 'Teacher';
    } else {
      return false;
    }
  }

  loggedIn() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {
      return this.authService.loggedIn(); 
    }
  }

  toEdit() {
    this.router.navigate(['teacher/courses/edit/', this.course.courseId]);
  }
}
