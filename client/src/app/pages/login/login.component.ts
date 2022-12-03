import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

import { AccountService } from 'src/app/services/account.service';

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  loginForm: FormGroup | null = null;

  constructor(
    private accountService: AccountService,
    private toastrService: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeLoginForm();
  }

  onSubmit() {
    this.accountService
      .login(this.loginForm?.value)
      .pipe(takeUntil(this.dispose$))
      .subscribe(
        () => {},
        (error) => this.toastrService.error(error.error)
      );
  }

  private initializeLoginForm() {
    this.loginForm = this.fb.group({
      email: new FormControl('', [
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(
          "(?=^.{6,10}$)(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*s).*$"
        ),
      ]),
    });
  }

  ngOnDestroy(): void {
    this.dispose$.next(null);
    this.dispose$.complete();
  }
}
