import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import {User } from '../models/models';
import { TokenApiModel } from '../models/token-api.model';
import { tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

 
  private baseUrl: string = 'https://localhost:7256/api/Auth/';
  private baseUrl1: string = 'https://localhost:7256/api/Bills/';
 //  userStatus: Subject<string> = new Subject();
  userStatus: BehaviorSubject<string> = new BehaviorSubject<string>(this.getUserStatus());
  private userSubject: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(this.getCurrentUserFromLocalStorage());

  public currentUser = this.userStatus.asObservable();
  private userPayload:any;
  private jwtHelper = new JwtHelperService();
  constructor(
    // private jwt: JwtHelperService,
    private http:HttpClient,
     private router: Router) { 
      this.userPayload = this.decodedToken();
      this.userStatus.next(this.getRoleFromToken());
  }
  
  private getCurrentUserFromLocalStorage(): User | null {
    const userJson = localStorage.getItem('nameid');
    return userJson ? JSON.parse(userJson) : null;
  }
  private getUserStatus(): string {
    return localStorage.getItem('role') || 'loggedOff';
  }
  signUp(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}register`,userobj)
  }
  login(userobj:any): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}login`,userobj)
  }
  // login(userobj: any): Observable<any> {
  //   return this.http.post<any>(`${this.baseUrl}login`, userobj).pipe(
  //     tap(response => {
  //       this.storeToken(response.token);
  //       this.userPayload = this.decodedToken();
  //       this.userStatus.next('loggedIn');
  //     })
  //   );
  // }
  
  logOut() {
    // localStorage.removeItem('access_token');
    // // this.userInfo = null;
    // this.userStatus.next('loggedOff');
    // this.userStateService.updateTokens(0);
    this.userStatus.next('loggedOff');
    localStorage.clear();
    this.userStatus.next('loggedOff');
    this.router.navigate(['login'])
  }

  // signOut(){
  //   localStorage.clear();
  //   this.userStatus.next('loggedOff');
  //   this.router.navigate(['login'])
  // }
  

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }
  storeRefreshToken(tokenValue: string){
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }
  getRefreshToken(){
    return localStorage.getItem('refreshToken')
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (token) {
      // Check if the token is expired
      return !this.jwtHelper.isTokenExpired(token);
    }
    return false;
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }

  getfullNameFromToken(){
    if(this.userPayload)
    return this.userPayload.name;
  }

  getRoleFromToken(){
    if(this.userPayload)
    return this.userPayload.role;
  }

  renewToken(tokenApi : TokenApiModel){
    return this.http.post<any>(`${this.baseUrl}refresh`, tokenApi)
  }

 getCurrentUserId(): number {
  const tokenPayload = this.decodedToken();
  return tokenPayload ? parseInt(tokenPayload.nameid, 10) : 0;
}
loadCurrentUser() {
  const user = JSON.parse(localStorage.getItem('nameid') || 'null');
  if (user) {
    this.userStatus.next(user);
  }
}
// getCurrentUser(): User {
//   const user = JSON.parse(localStorage.getItem('nameid') || 'null');
//   console.log(user);
//   return user ? user : null;
// }
getCurrentUser(){
  if(this.userPayload)
  return this.userPayload.nameid;
}
}

