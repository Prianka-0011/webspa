import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
 model:any={};
 registerForm:FormGroup;
 maxDate:Date;
 @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
  constructor(private accountServive:AccountService,private toastr:ToastrService,private fb:FormBuilder) { }

  ngOnInit(): void {
   this. initializeForm();
   this.maxDate=new Date();
   this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }
  initializeForm()
  {
    this.registerForm=this.fb.group({
      userName:['',Validators.required],
      gender:['male'],
      knownAs:['',Validators.required],
      dateOfBirth:['',Validators.required],
      city:['',Validators.required],
      country:['',Validators.required],
      password:['',[Validators.required,
      Validators.minLength(4),
      Validators.maxLength(10)]],
      
      confirmPassword:['',[Validators.required,this.matchValue('password')]]
    });
    this.registerForm.controls['password'].valueChanges.subscribe(()=>{
      this.registerForm.controls['confirmPassword'].updateValueAndValidity();
    })
  }
  matchValue(matchTo:string):ValidatorFn{
   return (control:AbstractControl)=>{
    return control?.value===control?.parent?.controls[matchTo].value?null:{isMatching:true};
   }
  }
  
  register(){
    console.log(this.registerForm.value)
    // this.accountServive.register(this.model).subscribe(res=>{
    //   this.cancel();
    // },error=>{
    //   this.toastr.error(error.error);
    // })
  }

  cancel(){ 
    this.cancelRegister.emit(false);
  }

}
