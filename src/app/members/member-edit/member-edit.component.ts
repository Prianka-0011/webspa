import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.scss']
})
export class MemberEditComponent implements OnInit {
  @ViewChild ('editForm') editForm:NgForm;
  user:User;
member:Member;
@HostListener('window:beforeunload',['event']) unloadNotication($event:any){

  if(this.editForm.dirty)
  {
    $event.returnValue=true;
  }
}
  constructor(private accountService:AccountService,private router:ActivatedRoute,private memberService:MembersService,
    private toaster:ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
  }

  ngOnInit(): void {
    this.loadMember()
  }

loadMember()
{
this.memberService.getMember(this.user.userName).subscribe(member=>{
  this.member=member;
})
}
updateMember(){
console.log(this.member);
this.toaster.success('Profile update success');
this.editForm.reset(this.member);
}
}
