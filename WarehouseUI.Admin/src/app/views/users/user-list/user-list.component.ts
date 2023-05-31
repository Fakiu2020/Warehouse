import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SortEvent } from '../../shared/directives/sortable.directive';
import { ModalDeleteComponent } from '../../shared/modal-delete/modal-delete.component';
import { ArticlePagination } from '../../shared/models/article-pagination';
import { FilterBase } from '../../shared/models/pagination';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  users = [];
  filters = new FilterBase();
  modalRef: BsModalRef;
  isLoading = false;
  searchForm: FormGroup;
  bsConfig = Object.assign({}, { containerClass: 'theme-blue' });

  constructor(private _userService: UserService, private formBuilder: FormBuilder,
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

  openRemoveModal(userId) {
    const params = Object.assign({}, { class: 'modal-danger modal-md', title: 'Usuario' });
    this.modalRef = this.modalService.show(ModalDeleteComponent, params);
    this.modalRef.content.onClose.subscribe(result => {
      if (result) { this.delete(userId); }
    });
  }



  //#region private methods

  private createSearchForm() {
    this.searchForm = this.formBuilder.group({
      criteria: [null],
    });
  }

  private delete(userId) {
    this._userService.delete(userId).subscribe(data => {
      this.filters = new FilterBase();
      this.getAll();
    }, error => {
      console.error(error);
    });
  }

  private getAll() {
    this.isLoading = true;
    this._userService.getAll(this.filters).subscribe(data => {
      this.users = data.entity;
      this.filters.totalItems = data.filters.totalItems;
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      console.error(error);
    });
  }

  //#endregion


}
