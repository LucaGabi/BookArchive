import { CommonModule } from '@angular/common';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { APIDataService } from './http-data.service';
import { HttpResponseHandler } from './http-response-handler.service';
import { HttpResponseInterceptor } from './http.interceptor';

@NgModule({
  imports: [CommonModule, HttpClientModule]
})
export class HttpServiceModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: HttpServiceModule,
      providers: [
        HttpResponseHandler,
        { provide: HTTP_INTERCEPTORS, useClass: HttpResponseInterceptor, multi: true }
      ]
    };
  }
}
