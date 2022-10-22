import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_modules/pagination';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {

members:Member[];
pagination:Pagination;
pageNumber=1;
pageSize=5;
  //members$:Observable<Member[]>;
  constructor(private memberService:MembersService) { }

  ngOnInit(): void {
    this.loadMembers()
  }
  loadMembers()
  {
  
   //this.members$=this.memberService.getMembers();
   this.memberService.getMembers(this.pageNumber,this.pageSize).subscribe(
    res=>{
      this.members=res.result;
      this.pagination=res.pagination
    }
    )
  }
  pageChangedeve(event:any)
  {
   this.pageNumber=event.page;
   this.loadMembers()
  }
}
