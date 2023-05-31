import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SortableHeaderDirective } from './directives/sortable.directive';



@NgModule({
  declarations: [SortableHeaderDirective],
  imports: [
    CommonModule
  ],  
  exports: [SortableHeaderDirective] 
})
export class SharedModule { }
