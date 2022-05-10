import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
 model:any={};
 
 @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
  constructor(private accountServive:AccountService) { }

  ngOnInit(): void {
  }
  register(){
    this.accountServive.register(this.model).subscribe(res=>{
      this.cancel();
    })
  }
  cancel(){
    
    this.cancelRegister.emit(false);
  }

}
