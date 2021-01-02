import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpHeaders
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable()
export class HttpResponseInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let headers = new HttpHeaders();
    headers = headers.set('Accept', 'application/json');

    if (!request.headers.has("Content-Type")) {
      headers = headers.set("Content-Type", "application/json");
    }
    // add a custom header
    const customReq = request.clone({ headers });

    // pass on the modified request object
    return next.handle(customReq).pipe(
      tap((ev: HttpEvent<any>) => {
        if (ev instanceof HttpResponse) {
          console.log('processing response', ev);
        }
      }),
      catchError(response => {
        if (response instanceof HttpErrorResponse) {
          console.log('Processing http error', response);
        }
        return throwError(response);
      })
    );
  }
}
