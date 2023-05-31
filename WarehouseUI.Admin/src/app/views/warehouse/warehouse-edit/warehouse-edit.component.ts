import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WarehouseService } from '../services/warehouse.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './warehouse-edit.component.html',
  styleUrls: ['./warehouse-edit.component.scss']
})
export class WarehouseEditComponent implements OnInit {
  editProductForm: FormGroup;
  parents  = [];
  children = [];
  measuresUnits: Array<string> = ['KG', 'LTS', 'M3'];


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private toastService: ToastrService,
    private warehouseService: WarehouseService) { }

  ngOnInit(): void {
    this.warehouseService.getById(this.route.snapshot.params['id']).subscribe(
      (data) => {
        this.editProductForm = this.fb.group({   
          warehouseId: [data.warehouseId, Validators.required],   
          name: [data.name, Validators.required],
          code: [data.code, Validators.required],
          address: [data.address, Validators.required],
          state: [data.state, Validators.required],
          county: [data.county],
          zip: [data.zip],
        });
        
      },
      (error) => {
        this.toastService.error(error);
      }
    );
    
    
  }

  update(){
    if (this.editProductForm.invalid) {
      return;
    }
    this.warehouseService.update(this.editProductForm.value).subscribe(
      next => {
        this.toastService.success('Successfully Edited!');
        this.router.navigate(['/warehouse']);
      },
      error => {
        this.toastService.error(error);
      });
  }


 
}
