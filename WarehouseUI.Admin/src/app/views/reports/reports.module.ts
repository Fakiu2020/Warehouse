import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';
import { NgSelectModule } from '@ng-select/ng-select'; 
import { ReportRoutingModule } from './report-routing.module';
import { ReportsComponent } from './reports.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


@NgModule({
  declarations: [ReportsComponent ],
  imports: [

    ReportRoutingModule,
    CommonModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(), 
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    NgSelectModule ,
    BsDatepickerModule.forRoot(),

  ],
  providers:[[DatePipe]]

})
export class ReportModule { }
