import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router:Router,private toaster:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
     catchError(err => {
       if (err) {
         switch(err.status)
         {
           case 400:
             if(err.error.errors){
               const modalStateError=[];
               for(const key in err.error.errors){
                 if(err.error.errors[key])
                 {
                  modalStateError.push(err.error.errors[key])
                 }
                
               }
               throw modalStateError.flat();
             }
             else{
               this.toaster.error(err.statusText,err.status);
             }
             break;
             case 401:
              this.toaster.error(err.statusText,err.status);
              break;
              case 404:
                this.toaster.error('bad request');
              break;
              case 500:
              const navigateExtra:NavigationExtras={state:{error:err.error}}
              this.router.navigateByUrl('/server-notfound',navigateExtra);
              break;
              default:
                this.toaster.error('Some thing unexpected wrong');
                break;

         }
        // remove Bearer token and redirect to login page
        // this.router.navigate(['/auth/login']);
       }
       return throwError( err );
     }));
 }
}
