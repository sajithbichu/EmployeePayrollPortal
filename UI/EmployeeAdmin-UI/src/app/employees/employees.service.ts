import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employees } from '../models/api-models/employees.model';
import { updateEmployeeRequest } from '../models/api-models/update.employee.request.model';
import { addEmployeeRequest } from '../models/api-models/add.employee.request';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  private baseApiUrl = 'https://localhost:7239';

  constructor(private httpClient: HttpClient) { }

  //Get all employee details
  getEmployees(): Observable<Employees[]>{
    return this.httpClient.get<Employees[]>(this.baseApiUrl + '/Employee');
  }

  //Get single employee details
  getEmployee(employeeId: string): Observable<Employees>{
    return this.httpClient.get<Employees>(this.baseApiUrl + '/Employee/' + employeeId);
  }

  updateEmployee(_employeeId: string, _updateRequest: Employees): Observable<Employees>  {

    const _updateEmployeeRequest: updateEmployeeRequest = {
      firstName: _updateRequest.firstName,
      lastName: _updateRequest.lastName,
      dateOfBirth: _updateRequest.dateOfBirth,
      email: _updateRequest.email,
      mobile: _updateRequest.mobile,
      genderId: _updateRequest.genderId,
      physicalAddress: _updateRequest.address.physicalAddress,
      postalAddress: _updateRequest.address.postalAddress
    }

    return this.httpClient.put<Employees>(this.baseApiUrl + '/Employee/' + _employeeId, _updateEmployeeRequest);

  }

  deleteEmployee(employeeId: string): Observable<Employees>{
    return this.httpClient.delete<Employees>(this.baseApiUrl + '/Employee/' + employeeId);
  }

  addEmployee(_addRequest: Employees): Observable<Employees>  {

    const _addEmployeeRequest: addEmployeeRequest = {
      firstName: _addRequest.firstName,
      lastName: _addRequest.lastName,
      dateOfBirth: _addRequest.dateOfBirth,
      email: _addRequest.email,
      mobile: _addRequest.mobile,
      genderId: _addRequest.genderId,
      physicalAddress: _addRequest.address.physicalAddress,
      postalAddress: _addRequest.address.postalAddress
    }

    return this.httpClient.post<Employees>(this.baseApiUrl + '/Employee/Add', _addEmployeeRequest);

  }

  uploadImage(employeeId: string, file: File): Observable<any>{
      const formData = new FormData();
      formData.append("profileImage", file);
      return this.httpClient.post(this.baseApiUrl + '/Employee/' + employeeId + '/upload-image',
        formData, {
          responseType : 'text'
        }
      );
  }

  getImagePath(relativePath: string){
    return this.baseApiUrl + "/" + relativePath;
  }

}



