import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.scss']
})
export class TestErrorsComponent implements OnInit {
  baseUrl='https://localhost:44348/api/'
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
get404Error(){
  this.http.get(this.baseUrl+'Buggy/not-found').subscribe(response=>{
    console.log(response);
  },error=>{
    console.log(error)
  })
}
get500Error(){
  this.http.get(this.baseUrl+'Buggy/server-error').subscribe(response=>{
    console.log(response);
  },error=>{
    console.log(error)
  })
}
get401Error(){
  this.http.get(this.baseUrl+'Buggy/auth').subscribe(response=>{
    console.log(response);
  },error=>{
    console.log(error)
  })
}
get400ValidationError(){
  this.http.get(this.baseUrl+'Buggy/GetBadRequest').subscribe(response=>{
    console.log(response);
  },error=>{
    console.log(error)
  })
}
}
