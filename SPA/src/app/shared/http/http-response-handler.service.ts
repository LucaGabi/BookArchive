import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NotificationsService } from 'angular2-notifications';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { ConfigService } from '../../app-config.service';

@Injectable()
export class HttpResponseHandler {
  constructor(
    private router: Router,
    private translateService: TranslateService,
    private notificationsService: NotificationsService,
    private configService: ConfigService
  ) { }

  /**
   * Global http error handler.
   *
   * @param error
   * @param source
   * @returns {ErrorObservable}
   */
  public onCatch(response: any, source: Observable<any>): Observable<any> {
    switch (response.status) {
      case 400:
        this.handleBadRequest(response);
        break;

      case 401:
        // this.handleUnauthorized(response);
        break;

      case 403:
        this.handleForbidden();
        break;

      case 404:
        this.handleNotFound(response);
        break;

      case 500:
        this.handleServerError();
        break;

      default:
        break;
    }

    return throwError(response);
  }

  /**
   * Shows notification errors when server response status is 401
   *
   * @param error
   */
  private handleBadRequest(responseBody: any): void {
    if (responseBody._body) {
      try {
        const bodyParsed = responseBody.json();
      } catch (error) {
        this.handleServerError();
      }
    } else {
      this.handleServerError();
    }
  }

  /**
   * Shows notification errors when server response status is 401 and redirects user to login page
   *
   * @param responseBody
   */
  private handleUnauthorized(responseBody: any): void {
    

  }

  /**
   * Shows notification errors when server response status is 403
   */
  private handleForbidden(): void {
  }

  /**
   * Shows notification errors when server response status is 404
   *
   * @param responseBody
   */
  private handleNotFound(responseBody: any): void {

    
  }

  /**
   * Shows notification errors when server response status is 500
   */
  private handleServerError(): void {
    
  }

  private showNotificationError(title: string, message: string): void {
   
  }
}
