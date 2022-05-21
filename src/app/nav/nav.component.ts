import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

 model:any={};
 
  constructor(public accountService:AccountService,private router:Router) { }

  ngOnInit(): void {
   
  }
  login(){
  console.log(this.model);
  this.accountService.login(this.model).subscribe(res=>{
   this.router.navigateByUrl('/members')
  });

  }
  logout()
  {
    this.accountService.logOut();
  }
}
