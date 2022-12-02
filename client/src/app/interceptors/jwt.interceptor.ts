import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, take } from 'rxjs';

import { AccountService } from '../services/account.service';
import { User } from '../models/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let currentUser!: User;

    console.log('IAM INCERTPRO 1::', currentUser);

    // this.accountService.currentUser$.subscribe((user) => (currentUser = user));

    this.accountService.currentUser$.subscribe((user) => {
      currentUser = user;
      console.log('FROM INSIDE CURRENTUSER::', currentUser);
      console.log('FROM INSIDE CURRENTUSER user::', user);
    });

    if (currentUser) {
      console.log('IAM INCERTPRO 2::', currentUser);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
        },
      });
    }

    return next.handle(request);
  }
}
