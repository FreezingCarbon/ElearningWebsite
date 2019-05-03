import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  isTeacher: boolean;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params['registerMode'] != null) {
        this.registerMode = params['registerMode'];
        this.isTeacher = params['isTeacher'];
      }
    });
  }

  registerToggleTeacher() {
    this.registerMode = true;
    this.isTeacher = true;
  }

  registerToggleStudent() {
    this.registerMode = true;
    this.isTeacher = false;
  }

  cancelRegisterMode(registerMode: boolean): void {
    this.registerMode = registerMode;
  }
}
