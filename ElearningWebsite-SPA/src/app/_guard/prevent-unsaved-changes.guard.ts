import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { CourseEditComponent } from '../course/course-edit/course-edit.component';

@Injectable()
export class PreventUsavedChanges implements CanDeactivate<CourseEditComponent> {
    canDeactivate(component:  CourseEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Are you sure you want to continue? Any unsaved changes will be lost');
        }
        return true;
    }
}
