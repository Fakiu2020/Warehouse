import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isLoading = false;

  /**
   *
   */
   constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.createForm();
  }



  private createForm() {
    this.loginForm = this.formBuilder.group({
      email: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  login () {
    this.loginForm.get('email').setErrors(null);
    if (this.loginForm.invalid) {
      return;
    }
    this.isLoading = true;
    this.authService.login(this.loginForm.value).subscribe(
      (data) => {
        this.router.navigate(['warehouse']);
        this.isLoading = false;
      },
      (error) => {
        this.isLoading = false;
        this.loginForm.get('email').setErrors({ incorrect: true, message: error});
      }
    );
  }
}

