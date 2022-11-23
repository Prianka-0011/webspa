import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { UserParams } from '../_models/userParams';
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

  constructor(private http:HttpClient) { }
  getMembers(userParams:UserParams){
 
  let params=this.getPaginationHeader(userParams.pageNumber,userParams.pageSize);
 params=params.append('minAge',userParams.minAge.toString())
 params=params.append('maxAge',userParams.maxAge.toString())
 params=params.append('gender',userParams.gender)
 params=params.append('orderBy',userParams.orderBy)

   return this.getPaginatedResult<Member[]>(this.baseUrl+'user',params);
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
  private getPaginatedResult<T>(url,params) {
    const  paginatedRetult:PaginatedResult<T>=new PaginatedResult<T>();
      return this.http.get<T>(url,{ observe: 'response', params }).pipe(
        map(response => {
          paginatedRetult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedRetult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedRetult;
        })
      );
    }
  
    private getPaginationHeader(pageNumber:number,pageSize:number)
    {
      let params=new  HttpParams();
      
        params=params.append('pageNumber',pageNumber.toString());
        params=params.append('pageSize',pageSize.toString());
      return params;
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