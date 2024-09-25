import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Student } from '../Models/student';
import { catchError } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiurl = "https://localhost:7245/api/Student";
  private departmentApiUrl = "https://localhost:7245/api/Department";
  private http = inject(HttpClient);

  constructor() {}

  getAllStudent() {
    return this.http.get<Student[]>(`${this.apiurl}/getAllStudent`).pipe(
      catchError(error => {
        console.error('Error fetching students:', error);
        return of([]); // Return an empty array or handle as needed
      })
    );
  }

//   addStudent(data: any) {
//     console.log('Adding student with data:', data);
//     return this.http.post<any>(`${this.apiurl}/Create`, data).pipe(
//       catchError(error => {
//         console.error('Error adding student:', error);
//         return of(null);
//       })
//     );
// }


// addStudent(data: any) {
//   console.log('Adding student with data:', data);
//   return this.http.post<any>(`${this.apiurl}/Create`, data).pipe(
//     catchError((error: HttpErrorResponse) => {
//       if (error.status === 400 && error.error.errors) {
//         // Map validation errors to form controls
//         return throwError(error.error.errors);
//       }
//       return throwError('An error occurred');
//     })
//   );
// }

addStudent(data: any) {
  console.log('Adding student with data:', data);
  return this.http.post<any>(`${this.apiurl}/Create`, data).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 400 && error.error.errors) {
        // Return the validation errors for handling in the component
        return throwError(error.error.errors);
      }
      return throwError('An error occurred');
    })
  );
}

  
  

getAllDepartments() {
  return this.http.get<any[]>(`${this.departmentApiUrl}/GetAllDepartments`).pipe(
    catchError(error => {
      console.error('Error fetching departments:', error);
      return of([]); // Return an empty array or handle as needed
    })
  );
}


deleteStudent(id: number): Observable<void> {
  const params = new HttpParams().set('id', id.toString());
  return this.http.delete<void>(`${this.apiurl}/Delete`, { params }).pipe(
    catchError(error => {
      console.error('Error deleting student:', error);
      return of(); // Handle error as needed
    })
  );
}


updateStudent(data: any): Observable<any> {
  console.log('Updating student with data:', data);
  return this.http.put<any>(`${this.apiurl}/UpdateStudent`, data).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 400 && error.error.errors) {
        console.log(error.error.errors)
        return throwError(error.error.errors);
      }
      return throwError('An error occurred');
    })
  );
}


}
