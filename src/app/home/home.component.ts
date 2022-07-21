import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
registerMode=false;
users:any;

  constructor() { }

  ngOnInit(): void {
   
  }
registerToggle()
{
  this.registerMode=!this.registerMode;
}

CancelRegisterMode(event:any)
{
this.registerMode=event;
}
}