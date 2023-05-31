
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ArticlePagination } from '../../shared/models/article-pagination';
import { FilterBase } from '../../shared/models/pagination';
import { SortEvent } from '../../shared/directives/sortable.directive';
import { ModalDeleteComponent } from '../../shared/modal-delete/modal-delete.component';
import { WarehouseService } from '../services/warehouse.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-product-list',
  templateUrl: './warehouse-list.component.html',
  styleUrls: ['./warehouse-list.component.scss']
})
export class WarehouseListComponent implements OnInit {
  products = [];
  filters = new FilterBase();
  modalRef: BsModalRef;
  isLoading = false;
  searchForm: FormGroup; 
  bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  

  constructor(private warehouseService: WarehouseService, private formBuilder: FormBuilder, 
              private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getAll();
    this.createSearchForm();
  }

  onSort({ column, direction }: SortEvent) {
    this.filters.SortDir = direction ? direction : null;
    this.filters.Sort = this.filters.SortDir ? column : null;
    this.getAll();
  }
  clearFilters() {
    this.filters = new FilterBase();
    this.createSearchForm();
    this.getAll();
  }

  search() {
    this.filters.criteria = this.searchForm.get('criteria').value;
    this.getAll();
  }

  // *********PAGINATIONS
  pageChanged(event: any): void {
    this.filters.page = event.page;
    this.getAll();
  }

  openRemoveModal(warehouseId) {
    const params = Object.assign({}, { class: 'modal-danger modal-md', title: 'Warehouse' });
    this.modalRef = this.modalService.show(ModalDeleteComponent, params);
    this.modalRef.content.onClose.subscribe(result => {
      if (result) { this.delete(warehouseId); }
    });
  }


  dowloadFile(wareHouse) {
    
    wareHouse.loadingExcel=true;
    this.warehouseService.exporToExcel({wareHouseId:wareHouse.warehouseId}).subscribe(response => {
      const blob = new Blob([response ], { type: response.type });
       saveAs(blob, wareHouse.fileName );
       wareHouse.loadingExcel=false;

    }, error => {
      wareHouse.loadingExcel=false;
    });
  }



  //#region private methods

  private createSearchForm() {
    this.searchForm = this.formBuilder.group({
      criteria: [null],
    });
  }

  private delete(warehouseId) {
    this.warehouseService.delete(warehouseId).subscribe(data => {
      this.filters = new ArticlePagination();
      this.getAll();
    }, error => {
      console.error(error);
    });
  }

  private getAll() {
    this.isLoading = true;
    this.warehouseService.getAll(this.filters).subscribe(data => {
      console.log(data)
      this.products = data.entity;
      this.filters.totalItems = data.filters.totalItems;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      console.error(error);
    });
  }

  //#endregion

}
