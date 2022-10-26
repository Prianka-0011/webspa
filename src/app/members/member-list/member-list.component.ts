import { Component, OnInit } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { Pagination } from 'src/app/_modules/pagination';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {

members:Member[];
pagination:Pagination;
userParams:UserParams;
user:User;;
  //members$:Observable<Member[]>;
  constructor(private memberService:MembersService,private accountService:AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
      this.user=user;
      this.userParams=new UserParams(user);
    })
   }

  ngOnInit(): void {
    this.loadMembers()
  }
  loadMembers()
  {
  
   //this.members$=this.memberService.getMembers();
   this.memberService.getMembers(this.userParams).subscribe(
    res=>{
      this.members=res.result;
      this.pagination=res.pagination
    }
    )
  }
  pageChangedeve(event:any)
  {
   this.userParams.pageNumber=event.page;
   this.loadMembers()
  }
}
