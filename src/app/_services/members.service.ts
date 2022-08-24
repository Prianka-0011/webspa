import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
// const httpHeader={
// headers: new HttpHeaders({
//   Authorization:'Bearer'+JSON.parse(localStorage.getItem('user'))?.token
// })
// }
@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl=environment.apiUrl;
members:Member[]=[];
  constructor(private http:HttpClient) { }
  getMembers(){
    if(this.members.length>0)
    return of(this.members);
    return this.http.get<Member[]>(this.baseUrl+'user')
    .pipe(
      map(members=>{
      this.members=members;
      return members
  })
    );
  }
  getMember(userName:string){
    const member=this.members.find(x=>x.userName===userName);
    if (member !==undefined)return of(member);
    return this.http.get<Member>(this.baseUrl+'user/'+userName);
  }
  updateMember(member:Member)
  {
    return this.http.put<Member>(this.baseUrl+'user',member)
    .pipe(
      map(()=>{
      const index=this.members.indexOf(member);
      this.members[index]=member;
  })
    );
  }
}
