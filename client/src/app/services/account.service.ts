import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { UserForLogin } from '../models/user-for-login';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  currentUser$ = new BehaviorSubject<User>(undefined!);

  constructor(private http: HttpClient, private router: Router) {}

  login(userForLogin: UserForLogin) {
    return this.http
      .post<User>(`${environment.apiUrl}/account/login`, userForLogin)
      .pipe(
        map((user) => {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser$.next(user);
          this.router.navigateByUrl('/');
        })
      );
  }

  loadCurrentUser(user: User) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${user.token}`);

    return this.http
      .get<User>(`${environment.apiUrl}/account/current-user`, { headers })
      .pipe(
        map((user) => {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser$.next(user);
        })
      );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser$.next(null!);
    this.router.navigateByUrl('/');
  }
}