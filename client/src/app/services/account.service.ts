import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ReplaySubject, map } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Address } from '../models/address';
import { User } from '../models/user';
import { UserForLogin } from '../models/user-for-login';
import { UserForRegister } from '../models/user-for-register';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  currentUser$ = new ReplaySubject<User>(1);

  constructor(private http: HttpClient, private router: Router) {}

  login(userForLogin: UserForLogin) {
    return this.http
      .post<User>(`${environment.apiUrl}/account/login`, userForLogin)
      .pipe(
        map((user) => {
          this.setUserToLocalStorage(user);
          this.router.navigateByUrl('/shop');
        })
      );
  }

  register(userForRegister: UserForRegister) {
    return this.http
      .post<User>(`${environment.apiUrl}/account/register`, userForRegister)
      .pipe(
        map((user) => {
          this.setUserToLocalStorage(user);
          this.router.navigateByUrl('/shop');
        })
      );
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(
      `${environment.apiUrl}/account/email-exists?email=` + email
    );
  }

  loadCurrentUser(user: User) {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${user.token}`);

    return this.http
      .get<User>(`${environment.apiUrl}/account/current-user`, { headers })
      .pipe(map((user) => this.setUserToLocalStorage(user)));
  }

  loadUserAddress() {
    return this.http.get<Address>(`${environment.apiUrl}/account/address`);
  }

  updateUserAddress(address: Address) {
    return this.http.post<Address>(
      `${environment.apiUrl}/account/address`,
      address
    );
  }

  logout() {
    this.router.navigateByUrl('/');
    localStorage.removeItem('user');
    this.currentUser$.next(null!);
  }

  private setUserToLocalStorage(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser$.next(user);
  }
}
