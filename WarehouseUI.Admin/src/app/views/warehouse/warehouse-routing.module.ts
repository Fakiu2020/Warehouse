import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseCreateComponent } from './warehouse-create/warehouse-create.component';
import { WarehouseEditComponent } from './warehouse-edit/warehouse-edit.component';
import { WarehouseListComponent } from './warehouse-list/warehouse-list.component';
import { AuthGuardService } from '../shared/guards/auth-guard';

const routes: Routes = [
  {
    path: '',
    component: WarehouseListComponent,
    data: {
      title: 'Warehouse',
      roles: ['User Manager']
    }
  },
  {
    path: 'create',
    component: WarehouseCreateComponent,
    data: {
      title: 'Warehouse / Create',
    }
  },
  {
    path: 'edit/:id',
    component: WarehouseEditComponent,
    data: {
      title: 'Warehouse / Edit',
    }
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseRoutingModule { }
