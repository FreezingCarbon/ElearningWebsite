import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/_models/course';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input('course') course: Course;
  uploader: FileUploader;
  baseUrl = environment.apiUrl;

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeUploader();
  }

  initializeUploader() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      this.uploader = new FileUploader({
        url: this.baseUrl + 'teacher/' + data['nameid'] + '/courses/' + this.course.courseId,
        authToken: 'Bearer ' + localStorage.getItem('token'),
        method: 'post',
        isHTML5: true,
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 100 * 1024 * 1024
      });
    }
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
    this.uploader.onSuccessItem =  (item, response, status, headers) => {
      if (response) {
        console.log(response);
        const res: Course = JSON.parse(response);
        this.course.avaUrl = res.avaUrl;
        this.alertify.success('Upload success');
      }
    };
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn() && data['role'] === 'Teacher';
    } else {
      return false;
    }
  }
}
