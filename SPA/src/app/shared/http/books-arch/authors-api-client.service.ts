import { Author } from './models/author.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpResponseHandler } from '@shared/http';

import { APIDataService } from '@shared/http';


@Injectable({
  providedIn:'root'
})
export class AuthorsApiClient extends APIDataService<Author, Number> {
  constructor(httpClient: HttpClient, responseHandler: HttpResponseHandler) {
    super('/authors', httpClient, responseHandler);
  }

}
