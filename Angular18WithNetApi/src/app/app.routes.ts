import { Routes } from '@angular/router';
import { StudentComponent } from './Component/student/student.component';

export const routes: Routes = [
    {
        path : "", component:StudentComponent

    },
    {
        path: "student", component:StudentComponent
    }
];
