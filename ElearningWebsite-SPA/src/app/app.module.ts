import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';

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
import { AuthGuard } from './_guard/auth.guard';
import { CourseslistComponent } from './course/courseslist/courseslist.component';
import { UserService } from './_services/user.service';
import { CourseCardComponent } from './course/course-card/course-card.component';
import { CoursesResolver } from './_resolvers/courses.resolver';
import { CourseNDetailComponent } from './course/course-n-detail/course-n-detail.component';
import { CourseNResolver } from './_resolvers/course-n.resolver';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      HomeComponent,
      CoursesComponent,
      AboutComponent,
      RegisterComponent,
      CourseslistComponent,
      CourseCardComponent,
      CourseNDetailComponent,
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      NgbModule,
      RouterModule.forRoot(appRoutes),
      PaginationModule.forRoot()
   ],
   providers: [
      AuthService,
      TeacherService,
      UserService,
      TeacherService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      CoursesResolver,
      CourseNResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
