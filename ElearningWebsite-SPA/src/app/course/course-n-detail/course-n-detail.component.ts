import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Course } from 'src/app/_models/course';
import { routerNgProbeToken } from '@angular/router/src/router_module';
import { AuthService } from 'src/app/_services/auth.service';
import { Video } from 'src/app/_models/video';

@Component({
  selector: 'app-course-n-detail',
  templateUrl: './course-n-detail.component.html',
  styleUrls: ['./course-n-detail.component.css']
})
export class CourseNDetailComponent implements OnInit {
  course: Course;
  videos: Video[];
  requirements: string[];
  descriptions: string[];

  constructor(private userService: UserService, private route: ActivatedRoute,
    private alertify: AlertifyService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.course = data['course'];
    });
    if(this.course.requirement) {
      this.requirements = this.course.requirement.split('\n');
    }
    if(this.course.description) {
      this.descriptions = this.course.description.split('\n');
    }
    this.videos = this.course.videos;
  }

  toRegister() {
    this.router.navigate(['student/register/', true, false]);
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn() && data['role'] === 'Teacher';
    } else {
      return false;
    }
  }

  loggedIn() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn();
    }
  }

  toEdit() {
    this.router.navigate(['teacher/courses/edit/', this.course.courseId, true]);
  }

  videoPlayTab(link: string) {
    window.open(link, '_blank');
  }
}
