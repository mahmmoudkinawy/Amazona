import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  AsyncValidatorFn,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { map, of, Subject, switchMap, takeUntil, timer } from 'rxjs';

import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  registerForm: FormGroup | null = null;
  errors: string[] = [];

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeRegisterForm();
  }

  onSubmit() {
    this.accountService
      .register(this.registerForm?.value)
      .pipe(takeUntil(this.dispose$))
      .subscribe(
        () => {},
        (error) => (this.errors = error)
      );
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService
            .checkEmailExists(control.value)
            .pipe(takeUntil(this.dispose$))
            .pipe(
              map((res) => {
                return res ? { emailExists: true } : null;
              })
            );
        })
      );
    };
  }

  private initializeRegisterForm() {
    this.registerForm = this.fb.group({
      displayName: new FormControl('', [Validators.required]),
      email: new FormControl(
        '',
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
        [this.validateEmailNotTaken()]
      ),
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
