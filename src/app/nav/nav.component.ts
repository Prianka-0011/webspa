import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
 model:any={};
 logIn=false;
  constructor(private accountService:AccountService) { }

  ngOnInit(): void {
  }
  login(){
  console.log(this.model);
  this.accountService.login(this.model).subscribe(res=>{
    this.logIn=true;
    console.log(res,"  ",this.logIn)
  },error=>{
    console.log(error)
  } );

  }
  logout()
  {
    this.logIn=false;
  }
}
