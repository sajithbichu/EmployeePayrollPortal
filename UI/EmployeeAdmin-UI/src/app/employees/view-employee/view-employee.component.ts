import { ConnectedOverlayPositionChange } from '@angular/cdk/overlay';
import { HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Employees } from 'src/app/models/ui-models/ui.employees.model';
import { Gender } from 'src/app/models/ui-models/ui.gender.model';
import { GenderService } from 'src/app/Services/gender.service';
import { EmployeesService } from '../employees.service';

@Component({
  selector: 'app-view-employee',
  templateUrl: './view-employee.component.html',
  styleUrls: ['./view-employee.component.css']
})
export class ViewEmployeeComponent implements OnInit {
  employeeId: string | null | undefined;

  _employee: Employees = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    email: '',
    mobile: 0,
    genderId: '',
    profileImageUrl:'',
    gender:{
      id:'',
      description:''
    },
    address: {
      id:'',
      physicalAddress:'',
      postalAddress:''
    }

  }

  _isNewStudent = false;
  _header = '';
  _displayProfileImageUrl = '';

    _genderList: Gender[] = [];

    @ViewChild('employeeDetailsForm') employeeDetailsForm? : NgForm;

  constructor(private readonly employeeService: EmployeesService,
    private readonly route: ActivatedRoute,
    private readonly genderService: GenderService,
    private snackbar: MatSnackBar,
    private _router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        this.employeeId = params.get('id');

        if (this.employeeId)
        {

          if (this.employeeId.toLowerCase() === 'Add'.toLowerCase())
          {
              //New Student
              this._isNewStudent = true;
              this._header = 'Add New Employee Details';
              this.setImage();
          }
          else
          {
              this._isNewStudent = false;
              this._header = 'Edit Employee Details';

              this.employeeService.getEmployee(this.employeeId)
              .subscribe(
                (successResponse) => {
                  this._employee = successResponse;
                  this.setImage();
                },
                (errorResponse) => {
                  this.setImage();
                }
              );
          }


          this.genderService.getGenderList()
          .subscribe(
            (successResponse) => {
              this._genderList = successResponse;
            }
          );
        }
      }
    );
  }


  onUpdate(): void {

    if (this.employeeDetailsForm?.form.valid)
    {
      this.employeeService.updateEmployee(this._employee.id, this._employee)
      .subscribe(
          (successResponse) => {
            this.snackbar.open("Employee updated successfully !", undefined, { duration : 3000});
            //show notification
          },
          (errorResponse) => {

            // log it

          }
      );
    }
 }

 onDelete(): void {

  // Employee service to delete
  this.employeeService.deleteEmployee(this._employee.id)
  .subscribe(
      (successResponse) => {
          this.snackbar.open("Employee deleted successfully !", undefined, { duration : 2000 });

          setTimeout(() => {
              this._router.navigateByUrl('Employees');
          }, 100);

      },
      (errorResponse) => {

        // log it

      }
  );


 }

 onAdd(): void {


    if (this.employeeDetailsForm?.form.valid)
    {

        this.employeeService.addEmployee(this._employee)
        .subscribe(
            (successResponse) => {
                this.snackbar.open("Employee added successfully !", undefined, { duration : 2000 });

                setTimeout(() => {
                  console.log(successResponse.id);
                  this._router.navigateByUrl("Employee/" + successResponse.id);
                    //this._router.navigateByUrl('Employees');
                }, 100);

            },
            (errorResponse) => {

              // log it

            }
        );

    }



 }

 uploadImage(event: any): void {
    if(this.employeeId)
    {
      const file: File = event.target.files[0];
      this.employeeService.uploadImage(this._employee.id, file)
        .subscribe(
          (successResponse)=> {
                this._employee.profileImageUrl = successResponse;
                this.setImage();

                this.snackbar.open("Employee image has been updated !", undefined, { duration : 2000});

          },
          (errorResponse)=> {

          }
        )
    }
 }

 private setImage(): void{
  if(this._employee.profileImageUrl)
  {
    //Fetch the image by URL
    this._displayProfileImageUrl=this.employeeService.getImagePath(this._employee.profileImageUrl);
  }
  else
  {
    //Display a default image

    this._displayProfileImageUrl='/assets/user.png';
  }
 }

}
