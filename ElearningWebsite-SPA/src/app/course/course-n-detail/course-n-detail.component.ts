import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Course } from 'src/app/_models/course';
import { routerNgProbeToken } from '@angular/router/src/router_module';

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
    private alertify: AlertifyService, private router: Router) { }

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

}
