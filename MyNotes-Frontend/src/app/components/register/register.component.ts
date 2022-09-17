import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  inputType: string = 'password';
  showPassword: string = 'Show password';

  registerViewModel = new FormGroup({
    username: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirmedPassword: new FormControl('', [Validators.required])
  });

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  togglePassword(): void {
    if (this.showPassword === 'Show password') {
      this.inputType = 'text';
      this.showPassword = 'Hide password';
    } else {
      this.inputType = 'password';
      this.showPassword = 'Show password';
    }
  }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!')
  }
}
