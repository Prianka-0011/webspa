import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_modules/pagination';
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
paginatedRetult:PaginatedResult<Member[]>=new PaginatedResult<Member[]>();
  constructor(private http:HttpClient) { }
  getMembers(page?:number,itemsPerPage?:number){
 
  let params=new  HttpParams();
  if(page!=null && itemsPerPage !=null)
  {
    params=params.append('pageNumber',page.toString());
    params=params.append('pageSize',itemsPerPage.toString());
  }
   return this.http.get<Member[]>(this.baseUrl + 'user',{observe:'response',params}).pipe(
      map(response=>{
        this.paginatedRetult.result=response.body;
        if(response.headers.get('Pagination') !==null)
        {
          this.paginatedRetult.pagination=JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedRetult;
      })
  )
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
  deletePhoto(photoId){
   return this.http.delete(this.baseUrl+'user/delete-photo/'+photoId);
  }
  setMainPhoto(photoId:number)
  {
    return this.http.put(this.baseUrl+'user/set-mail-photo/'+photoId,{});
  }
}
 //   if(this.members.length>0)
  //   return of(this.members);
  //   return this.http.get<Member[]>(this.baseUrl+'user')
  //   .pipe(
  //     map(members=>{
  //     this.members=members;
  //     return members
  // })
  //   );