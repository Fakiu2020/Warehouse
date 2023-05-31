import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WarehouseService } from '../services/warehouse.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './warehouse-create.component.html',
  styleUrls: ['./warehouse-create.component.scss'],
})
export class WarehouseCreateComponent implements OnInit {
  parents = [];
  children = [];
  isLoading = false;
  checkingAddressLocation = false;
  createProductForm: FormGroup;
  mimeTypeAllow: Array<any> = [
    'application/vnd.ms-excel',
    'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  ];

  resultAddress={
    address: null,
    name:null,
    lat:null,
    lng:null,
  };
  zoom: number = 14;


  constructor(
    private warehouseService: WarehouseService,
    private router: Router,
    private toastService: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  onFileChange(event) {
  
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      console.log(file)
      if (!this.mimeTypeAllow.some(x => x === file.type)) {
        this.toastService.error('File format not allowed')
        return;
      }
      
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        console.log(reader)
        this.createProductForm.get('fileData').setValue(reader.result);
        this.createProductForm.get('mymeType').setValue(file.type);
        this.createProductForm.get('fileName').setValue(file.name);
      };
      
    }
  }


  validateAddress(){
    
    this.checkingAddressLocation = true;
    
    this.warehouseService.getDetailAddressLocation(this.createProductForm.get('address').value).subscribe(data => {
      
      this.resultAddress = {
        address: data.result.address,
        name:data.result.name,
        lat:data.result.point.coordinates[0],
        lng:data.result.point.coordinates[1],
      };
      this.checkingAddressLocation = false;
     }, error => {
       this.checkingAddressLocation = false;
       console.error(error);
       this.toastService.error(error);
     });
  }
 

  create() {
    if (this.createProductForm.invalid) {return; }
    this.isLoading = true;
    this.warehouseService.create(this.createProductForm.value).subscribe(() => {
       this.toastService.success('Successfully Created!');
       this.isLoading = false;
     }, error => {
       this.toastService.error(error);
       this.isLoading = false;
     }, () => {
         this.router.navigate(['/warehouse']);
     });
 }


 confirmAddress(){
  this.createProductForm.controls.latitude.setValue(this.resultAddress.lat);
  this.createProductForm.controls.longitude.setValue(this.resultAddress.lng);
    this.resultAddress={
      address: null,
      name:null,
      lat:null,
      lng:null,
    };
  }


  private createRegisterForm() {
    this.createProductForm = this.fb.group({
      name: [null, Validators.required],
      code: [null, Validators.required],
      address: [null, Validators.required],
      state: [null, Validators.required],
      latitude: [null, Validators.required],
      longitude: [null, Validators.required],
      fileName: [null],
      fileData: [null],
      mymeType: [null],
      county: [null],
      zip: [null],
    });
  }

 


  
}
