import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TeacherService } from 'src/app/_services/teacher.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-course-create',
  templateUrl: './course-create.component.html',
  styleUrls: ['./course-create.component.css']
})
export class CourseCreateComponent implements OnInit {
  model: any = {};
  constructor(private router: Router, private teacherService: TeacherService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  createCourse() {
    this.teacherService.addCourse(this.model).subscribe((data) => {
      this.alertify.success('Created');
      this.router.navigate(['/teacher/courses/', data['courseId']]);
    }, error => {
      this.alertify.error(error);
    });
  }

  cancel() {
    this.router.navigate(['/teacher/courses']);
  }

}
