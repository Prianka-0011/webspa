import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { Observable, observable } from 'rxjs';

import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

 model:any={};
 
  constructor(public accountService:AccountService,private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {
   
  }
  login(){
  console.log(this.model);
  this.accountService.login(this.model).subscribe(res=>{
   this.router.navigateByUrl('/members')
  },error=>{
    this.toastr.error(error.error);
  });

  }
  logout()
  {
    this.accountService.logOut();
    this.router.navigateByUrl('/')
    
  }
}
