import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError  } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor  {
  constructor(
    private authService: AuthService, 
    private router: Router) 
    {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      setHeaders: {
        'Authorization': this.authService.getAuthorizationHeaderValue()
      },
    });

    return next.handle(req).pipe(
      map(this.eventResponse),
      catchError(this.errorHandler.bind(this))
    );
  }

  eventResponse(event: HttpEvent<any>): HttpEvent<any>
  {
    //console.log('event -> ', event)
    return event;
  }

  errorHandler(error: HttpErrorResponse) {
    let data = {
                    reason: error ? error.error ? error.error.reason : error.message : error.message,
                    status: error.status
                };

    //console.error("CUSTOM ERROR ", data);

    //if(data.status === 0) this.router.navigateByUrl('error/6');

    return throwError(error)
  }
}
