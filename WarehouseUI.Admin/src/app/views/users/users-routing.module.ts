import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserCreateComponent } from './user-create/user-create.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserListComponent } from './user-list/user-list.component';

const routes: Routes = [
  {
    path: '',
    component: UserListComponent,
    data: {
      title: 'Users',
    }
  },
  {
    path: 'create',
    component: UserCreateComponent,
    data: {
      title: 'Warehouse / Create',
    }
  },
  {
    path: 'edit/:id',
    component: UserEditComponent,
    data: {
      title: 'User / Edit',
    }
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
