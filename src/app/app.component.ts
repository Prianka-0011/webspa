import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Datingwebspa';
  constructor(private http:HttpClient){

  }
  users:any;
  ngOnInit(): void {
    
this.getUser();
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

