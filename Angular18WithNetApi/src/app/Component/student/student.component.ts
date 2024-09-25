import { Component, ElementRef, OnInit, ViewChild  } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Student } from '../../Models/student';
import { CommonModule } from '@angular/common';
import { StudentService } from '../../Services/student.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']  // Corrected to styleUrls for proper Angular style handling
})
export class StudentComponent implements OnInit {
  @ViewChild('addStudentModal') modal: ElementRef | undefined;
  @ViewChild('updateStudentModal') modal1: ElementRef | undefined;

  studentList: Student[] = [];
  departmentList: any[] = [];
  studentForm: FormGroup;

  constructor(private fb: FormBuilder, private studentService: StudentService) {
    this.studentForm = this.createForm();
    this.getAllDepartments();  // Fetch departments on initialization
  }
   

  ngOnInit(): void {
    this.getAllStudent();
  }

  createForm(): FormGroup {
    return this.fb.group({
      id: [0],
      firstname: [''],
      lastname: [''],
      regno: [''],
      email: [''],
      dateofbirth: [''],
      departmentId: ['']
    });
  }
  



  openModal(){
    const studentModal = document.getElementById('addStudentModal');
    if (studentModal != null) {
      studentModal.style.display = 'block';
    }
  }

  closeModal1(){
    if (this.modal1 != null) {
      this.modal1.nativeElement.style.display = 'none';

      this.studentForm.reset({
        id: 0,
        firstname: '',
        lastname: '',
        regno: '',
        email: '',
        dateofbirth: '',
        departmentId: ''
      }, { emitEvent: false });
    }
  }

  // Close the modal
  crossModal1(){
    if (this.modal1 != null) {
      this.modal1.nativeElement.style.display = 'none';
      this.studentForm.reset({
        id: 0,
        firstname: '',
        lastname: '',
        regno: '',
        email: '',
        dateofbirth: '',
        departmentId: ''
      }, { emitEvent: false });
    }
  }
  

  // Close the modal
  closeModal(){
    if (this.modal != null) {
      this.modal.nativeElement.style.display = 'none';

      this.studentForm.reset({
        id: 0,
        firstname: '',
        lastname: '',
        regno: '',
        email: '',
        dateofbirth: '',
        departmentId: ''
      }, { emitEvent: false });
    }
  }

  // Close the modal
  crossModal(){
    if (this.modal != null) {
      this.modal.nativeElement.style.display = 'none';
      this.studentForm.reset({
        id: 0,
        firstname: '',
        lastname: '',
        regno: '',
        email: '',
        dateofbirth: '',
        departmentId: ''
      }, { emitEvent: false });
    }
  }



// Fetch all students
getAllStudent(): void {
  this.studentService.getAllStudent().subscribe(
    (res: any) => { // Change Student[] to any
      console.log('Student Response:', res); // Log the response
      if (res.isSuccess) {
        this.studentList = res.data; // Set studentList to the data property
      } else {
        console.error('Error fetching students:', res.errorMessages);
        Swal.fire('Error', res.errorMessages || 'An error occurred while fetching students.', 'error'); // Show error message
      }
    },
    error => {
      console.error('Error fetching students:', error);
      Swal.fire('Error', 'An unexpected error occurred while fetching students.', 'error'); // Show error message
    }
  );
}


  deleteStudent(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.studentService.deleteStudent(id).subscribe(() => {
          this.studentList = this.studentList.filter(student => student.id !== id);
          Swal.fire('Deleted!', 'Your student has been deleted.', 'success');
        }, error => {
          Swal.fire('Error!', 'There was a problem deleting the student.', 'error');
        });
      }
    });
  }


  // Method to open modal and load student data for updating
  openUpdateModal(student: Student): void {
    // Populate the form with student data
    this.studentForm.patchValue({
      id: student.id,
      firstname: student.firstName,
      lastname: student.lastName,
      regno: student.regNo,
      email: student.email,
      dateofbirth: student.dateOfBirth ? this.formatDate(student.dateOfBirth) : '',
      departmentId: student.departmentId
    });
  
    // Open the modal
    const studentModal = document.getElementById('updateStudentModal');
    if (studentModal) {
      studentModal.style.display = 'block';
    }
  }
  
  private formatDate(dateString: any): string {
    const date = new Date(dateString);
    return date.toISOString().split('T')[0];  // Format to YYYY-MM-DD
  }



  onUpdate(): void {
    console.log('Update method called');
    
    if (this.studentForm.valid) {
      const formValues = this.studentForm.value;
  
      const studentData = {
        id: formValues.id,
        firstName: formValues.firstname,
        lastName: formValues.lastname,
        regNo: formValues.regno,
        email: formValues.email,
        dateOfBirth: formValues.dateofbirth,
        departmentId: formValues.departmentId,
        departmentName: ''  // Optional
      };
  
      this.studentService.updateStudent(studentData).subscribe(
        response => {
          console.log('Update response:', response);
          Swal.fire('Success!', 'Student updated successfully.', 'success');
          this.getAllStudent();  // Refresh the student list
          this.closeModal1();  // Close the modal after updating
        },
        error => {
          console.error('Error updating student:', error);
          //debugger
          // Handle server-side validation errors
          if (error && error.FirstName) {
            console.log('Backend Validation Errors: ++++++++++++', error.FirstName);
            this.studentForm.controls['firstname'].setErrors({ serverError: error.FirstName });
          }
          if (error && error.LastName) {
            this.studentForm.controls['lastname'].setErrors({ serverError: error.LastName });
          }

          if (error && error.RegNo) {
            this.studentForm.controls['regNo'].setErrors({ serverError: error.RegNo });
          }
          // Handle other fields similarly
          Swal.fire('Error', 'Please fix the errors in the form.', 'error');
        }
      );
    } else {
      Swal.fire('Invalid Input', 'Please fill out all required fields correctly.', 'warning');
    }
  }
  


  
  
  
  
  
  
  // Fetch all departments
  getAllDepartments(): void {
    this.studentService.getAllDepartments().subscribe(
      (res: any[]) => {
        console.log('Department Response:++++++++++++++++++++++++++++++++++++', res);
        this.departmentList = res;
      },
      error => {
        console.error('Error fetching departments:', error);
      }
    );
  }
 

onSubmit(): void {
  if (this.studentForm.valid) {
    const formValues = this.studentForm.value;

    const studentData = {
      id: formValues.id,
      regNo: formValues.regno,
      firstName: formValues.firstname,
      lastName: formValues.lastname,
      dateOfBirth: formValues.dateofbirth,
      email: formValues.email,
      departmentId: formValues.departmentId,
      departmentName: '' // Optional: Set if necessary
    };

    debugger
    this.studentService.addStudent(studentData).subscribe(
      response => {
        console.log('Add student response:', response); // Check the response
        
        // Check if the response contains an event message
        const eventMessage = response.eventMessage; // Adjust according to your API's response structure

        if (eventMessage) {
          Swal.fire('Success!', eventMessage, 'success');
        } else {
          Swal.fire('Success!', 'Student added successfully.', 'success');
        }

        this.getAllStudent();  // Refresh student list
        this.getAllDepartments();  // Refresh department list
        this.studentForm.reset();  // Reset the form
        this.closeModal();  // Close the modal
      },
      error => {
        console.log('Error adding student: ++++', error);
        
        // Handle server-side validation errors
        if (error && error.error && error.error.errors) {
          Object.keys(error.error.errors).forEach(field => {
            const control = this.studentForm.get(field);
            if (control) {
              control.setErrors({ serverError: error.error.errors[field] });
            }
          });
        } else {
          const errorMessage = error.error?.message || 'An unexpected error occurred.'; // Fallback error message
          Swal.fire('Error', errorMessage, 'error');
        }
      }
    );
  } else {
    Swal.fire('Invalid Input', 'Please fill out all required fields correctly.', 'warning');
  }
}

}
