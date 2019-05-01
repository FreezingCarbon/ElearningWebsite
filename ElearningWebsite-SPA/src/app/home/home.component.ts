import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  isTeacher: boolean;
  constructor() { }

  ngOnInit() {
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
