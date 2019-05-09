import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FileUploadModule } from 'ng2-file-upload';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { CoursesComponent } from './course/courses/courses.component';
import { AboutComponent } from './about/about.component';
import { AuthService } from './_services/auth.service';
import { TeacherService } from './_services/teacher.service';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptor, ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { appRoutes } from './routes';
import { UserService } from './_services/user.service';
import { CourseCardComponent } from './course/course-card/course-card.component';
import { CoursesResolver } from './_resolvers/courses.resolver';
import { CourseNDetailComponent } from './course/course-n-detail/course-n-detail.component';
import { CourseNResolver } from './_resolvers/course-n.resolver';
import { TeacherAuthGuard } from './_guard/teacher-auth.guard';
import { TCoursesResolver } from './_resolvers/t-courses.resolver';
import { CourseEditComponent } from './course/course-edit/course-edit.component';
import { EditCourseResolver } from './_resolvers/edit-course.resolver';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PreventUsavedChanges } from './_guard/prevent-unsaved-changes.guard';
import { VideoEditComponent } from './course/video-edit/video-edit.component';
import { PhotoEditorComponent } from './course/photo-editor/photo-editor.component';
import { CourseCreateComponent } from './course/course-create/course-create.component';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      HomeComponent,
      CoursesComponent,
      AboutComponent,
      RegisterComponent,
      CourseCardComponent,
      CourseNDetailComponent,
      CourseEditComponent,
      PhotoEditorComponent,
      VideoEditComponent,
      CourseCreateComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      NgbModule,
      RouterModule.forRoot(appRoutes),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      FileUploadModule
   ],
   providers: [
      AuthService,
      TeacherService,
      UserService,
      TeacherService,
      ErrorInterceptorProvider,
      AlertifyService,
      TeacherAuthGuard,
      PreventUsavedChanges,
      CoursesResolver,
      CourseNResolver,
      TCoursesResolver,
      EditCourseResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
