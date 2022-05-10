import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { empty, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource=new ReplaySubject<User>(1);
  currentUser$=this.currentUserSource.asObservable();
  baseUrl='https://localhost:44348/api/'
  constructor(private http:HttpClient) { }
  login(model:any){
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe(    
      map((res:User)=>{
        const user=res;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model:any){
    return this.http.post<User>(this.baseUrl+'account/register',model).pipe(    
      map((res:User)=>{
        const user=res;
        if(user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  setCurrentUser(user:User){
this.currentUserSource.next(user);
  }
  logOut(){
    localStorage.removeItem('user');
   this.currentUserSource.next(null);
  }
}
