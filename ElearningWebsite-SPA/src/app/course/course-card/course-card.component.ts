import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { Router } from '@angular/router';

@Component({
  selector: 'app-course-card',
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnInit {
  @Input() course: Course;

  constructor(private router: Router) { }

  ngOnInit() {
  }
}
