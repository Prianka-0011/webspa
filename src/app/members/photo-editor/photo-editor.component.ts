import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';

const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';
@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit {
  baseUrl=environment.apiUrl;
  uploader:FileUploader;
  hasBaseDropZoneOver=false;
  user:User;
  
  constructor(private accountService:AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
  }
  
@Input() member:Member;
  ngOnInit(): void {
    
    this.initializeUploader()
  }
  fileOverBase(e:any)
  {
    this.hasBaseDropZoneOver=e;
  }
initializeUploader()
{
  this.uploader=new FileUploader({
   url:this.baseUrl+'user/addphoto',
   authToken: `Bearer ${this.user.token}`,
   isHTML5:true,
   allowedFileType:['image'],
   removeAfterUpload:true,
   autoUpload:false,
   maxFileSize:10*1024*1024,
   
  })
  this.uploader.onAfterAddingAll=(file)=>{
    file.withCredentials=false;
  }
  this.uploader
  .onSuccessItem=(item,response,status,headers)=>{
if(response){
  const photo=JSON.parse(response);
  this.member.photos.push(photo);
}
}
}
}
