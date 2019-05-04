import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Course } from 'src/app/_models/course';
import { NgForm } from '@angular/forms';
import { TeacherService } from 'src/app/_services/teacher.service';

@Component({
  selector: 'app-course-edit',
  templateUrl: './course-edit.component.html',
  styleUrls: ['./course-edit.component.css']
})
export class CourseEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  course: Course;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute, private alertify: AlertifyService, private teacherService: TeacherService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.course = data['course'];
    });
    console.log(this.course);
  }

  updateCourse() {
    const data = JSON.parse(localStorage.getItem('user'));
    if(data) {

      this.teacherService.updateCourse(data['nameid'], this.course.courseId, this.course).subscribe(next => {
        this.alertify.success('Course updated sucessfully');
      }, error => {
        this.alertify.error(error);
      })
    }
  }

}
