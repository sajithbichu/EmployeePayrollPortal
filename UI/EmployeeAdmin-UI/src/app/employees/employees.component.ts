import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Employees } from '../models/ui-models/ui.employees.model';
import { EmployeesService } from './employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  _employees: Employees[] = [];
  _displayedColumns: string[] = ['firstName','lastName','dateOfBirth','email','mobile','gender','edit'];
  _filterString = '';

  //Set the datasource for Mat table to be diplayed
  _dataSource: MatTableDataSource<Employees> = new MatTableDataSource<Employees>();

  //set pagination
  @ViewChild(MatPaginator) _matPaginator!: MatPaginator;
  @ViewChild(MatSort) _matSort!: MatSort;

  constructor(private _EmployeesService: EmployeesService) { }

  ngOnInit(): void {

    // Om page INI call API to get the employee details

    this._EmployeesService.getEmployees()
      .subscribe(

        (successResponse) => {
          //console.log(successResponse);

          //Assiging the response to the UI model for best practice
          this._employees = successResponse;
          this._dataSource = new MatTableDataSource<Employees>(this._employees);

          if (this._matPaginator)
          {
            this._dataSource.paginator = this._matPaginator;
          }

          if (this._matSort)
          {
            this._dataSource.sort = this._matSort;
          }
        },
        (errorResponse) => {
          console.log(errorResponse);
        }

      );

  }

  //Data Filtering
  filterEmployees() {
    this._dataSource.filter= this._filterString.trim().toLowerCase();
  }

}
