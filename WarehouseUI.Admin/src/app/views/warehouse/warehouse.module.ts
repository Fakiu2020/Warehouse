import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseRoutingModule } from './warehouse-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';
import { WarehouseCreateComponent } from './warehouse-create/warehouse-create.component';
import { WarehouseEditComponent } from './warehouse-edit/warehouse-edit.component';
import { WarehouseListComponent } from './warehouse-list/warehouse-list.component';
import { NgSelectModule } from '@ng-select/ng-select'; 
import { AgmDirectionModule } from 'agm-direction';
import { AgmCoreModule } from '@agm/core';


@NgModule({
  declarations: [WarehouseListComponent, WarehouseCreateComponent, WarehouseEditComponent ],
  imports: [

    WarehouseRoutingModule,
    CommonModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(), 
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    NgSelectModule ,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyAA4WMD_eSM2lBwx9qbstWqTT8nI8pQHes'
    }),
    AgmDirectionModule
  ],
})
export class ProductModule { }
