import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpResponseHandler } from '@shared/http';
import { catchError } from 'rxjs/operators';

import { APIDataService } from '@shared/http';
import { IAPIResponse } from '../IAPIResponse';
import { IBookApiService } from './interfaces/IBookApiClient';
import { Author } from './models/author.model';
import { Book } from './models/book.model';




@Injectable({
  providedIn: 'root'
})
export class BooksApiClient extends APIDataService<Book, Number> implements IBookApiService {
  constructor(httpClient: HttpClient, responseHandler: HttpResponseHandler) {
    super('/books', httpClient, responseHandler);
  }

  getBookAuthors(id: number) {
    return this.httpClient.get(`${this.url}/${id}/bookAuthors`)
      .pipe<IAPIResponse<Author[]>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

  public create(entity: Book) {
    let form = new FormData();
    form.set('coverImage', entity.coverImage);
    form.set('request', JSON.stringify(entity));
    // entity.coverImage = null;
    return this.httpClient
      .post(`${this.url}`, form, {
        headers: { 'content-type': 'multipart/form-data' }
      })
      .pipe<IAPIResponse<Book>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

  public update(entity: Book) {
    let form = new FormData();
    form.set('coverImage', entity.coverImage);
    form.set('request', JSON.stringify(entity));
    // entity.coverImage = null;
    debugger;
    return this.httpClient
      .put(`${this.url}`, form, {
        headers: { 'content-type': 'multipart/form-data' }
      })
      .pipe<IAPIResponse<Book>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

}
