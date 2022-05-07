import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Datingwebspa';
  constructor(private http:HttpClient,private accountService:AccountService){

  }
  users:any;
  ngOnInit(): void {
    this.setCurrentUser();
    this.getUser();
    }
    setCurrentUser(){
     const user:User=JSON.parse(localStorage.getItem('user'));
     this.accountService.setCurrentUser(user);
        }
    getUser()
    {
      this.http.get('https://localhost:44348/api/user').subscribe(response=>{
        this.users=response;
        console.log("poku",this.users)
      }
    
      );
    }
  }

