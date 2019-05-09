import { Component, OnInit, Input } from '@angular/core';
import { Video } from 'src/app/_models/video';
import { environment } from 'src/environments/environment';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { TeacherService } from 'src/app/_services/teacher.service';
import { FileUploader } from 'ng2-file-upload';
import { AuthService } from 'src/app/_services/auth.service';
import { ActivatedRoute, Router, ActivatedRouteSnapshot } from '@angular/router';

@Component({
  selector: 'app-video-edit',
  templateUrl: './video-edit.component.html',
  styleUrls: ['./video-edit.component.css']
})
export class VideoEditComponent implements OnInit {
  @Input('videos') videos: Video[];
  @Input('courseId') courseId: number;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private teacherService: TeacherService, private alertify: AlertifyService,
    private authService: AuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.initializeUploader();
    this.route.params.subscribe(data => {
      this.courseId = data['courseId'];
    });
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      this.uploader = new FileUploader({
        url: this.baseUrl + 'teacher/' + data['nameid'] + '/courses/' + this.courseId + '/videos',
        authToken: 'Bearer ' + localStorage.getItem('token'),
        method: 'post',
        isHTML5: true,
        allowedFileType: ['video'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 400 * 1024 * 1024
      });
    }
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };
    this.uploader.onSuccessItem =  (item, response, status, headers) => {
      if (response) {
        console.log(response);
        const res: Video = JSON.parse(response);
        const video: Video = {
          videoId: res[0].videoId,
          videoUrl: res[0].videoUrl,
          createdDate: res[0].createdDate,
          publicId: res[0].publicId
        };
        this.alertify.success('Upload complete');
        this.videos.push(video);
      }
    };
  }

  deleteVideo(videoId: number) {
    this.teacherService.deleteVideo(this.courseId , videoId).subscribe(next => {
      this.alertify.success('Deleted');
      this.videos.slice(this.videos.findIndex(v => v.videoId === videoId), 1);
    }, error => {
      this.alertify.error(error);
    });
  }

  isTeacher() {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data) {
      return this.authService.loggedIn() && data['role'] === 'Teacher';
    } else {
      return false;
    }
  }

  videoPlayTab(link: string) {
    window.open(link, '_blank');
  }
}
