import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeesComponent } from './employees/employees.component';
import { ViewEmployeeComponent } from './employees/view-employee/view-employee.component';

const routes: Routes = [
  {
    path:'',
    component: EmployeesComponent
  },
  {
    path: 'Employees',
    component: EmployeesComponent
  },
  {
    path: 'Employee/:id',
    component: ViewEmployeeComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
