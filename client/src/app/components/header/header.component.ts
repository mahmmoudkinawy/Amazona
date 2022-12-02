import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';

import { Basket } from 'src/app/models/basket';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  private readonly dispose$ = new Subject();
  basket$ = this.basketService.basket$;
  currentUser$ = this.accountService.currentUser$;

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  logout() {
    this.accountService.logout();
  }

  //I know this localStorage for the token! is very dangerous, but will handle it later and make as a cookie
  private loadCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user') as string);
    if (user) {
      this.accountService
        .loadCurrentUser(user)
        .pipe(takeUntil(this.dispose$))
        .subscribe();
    }
  }

  private loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService
        .loadBasket(basketId)
        .pipe(takeUntil(this.dispose$))
        .subscribe();
    }
  }

  ngOnDestroy(): void {
    this.dispose$.next(null);
    this.dispose$.complete();
  }
}
