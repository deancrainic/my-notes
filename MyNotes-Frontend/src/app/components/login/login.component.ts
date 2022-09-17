import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  inputType: string = 'password';
  showPassword: string = 'Show password';

  constructor() { }

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
}
