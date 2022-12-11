import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';

import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  addressForm: FormGroup | null = null;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.createAddressFrom();
    this.loadAddressFormValues();
  }

  private loadAddressFormValues() {
    this.accountService
      .loadUserAddress()
      .pipe(takeUntil(this.dispose$))
      .subscribe(
        (address) => {
          if (address) {
            this.addressForm?.patchValue(address);
          }
        },
        (error) => {
          console.log('ERROR::', error);
        }
      );
  }

  private createAddressFrom() {
    this.addressForm = this.fb.group({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      street: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      state: new FormControl('', [Validators.required]),
      zipCode: new FormControl('', [Validators.required]),
    });
  }

  ngOnDestroy(): void {
    this.dispose$.complete();
    this.dispose$.next(null);
  }
}
