import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';

import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  registerForm: FormGroup | null = null;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.initializeRegisterForm();
  }

  onSubmit() {
    this.accountService.register(this.registerForm?.value).subscribe();
  }

  private initializeRegisterForm() {
    this.registerForm = new FormGroup({
      displayName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
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
