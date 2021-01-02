import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

import { HttpResponseHandler } from './http-response-handler.service';
import { IAPIDataService } from './IAPIDataService';
import { IAPIResponse } from './IAPIResponse';


@Injectable({
  providedIn: 'root'
})
export abstract class APIDataService<TEntity, TKey> implements IAPIDataService<TEntity, TKey>{
  url: string;
  constructor(
    url: string,
    protected httpClient: HttpClient,
    protected responseHandler: HttpResponseHandler
  ) { 
    this.url=environment.apiBase + url;
  }

  getOneById(id: TKey) {
    return this.httpClient.get<IAPIResponse<TEntity>>(this.url + '/' + id);
  }

  public getAll() {
    return this.httpClient
      .get(this.url)
      .pipe<IAPIResponse<TEntity[]>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

  public create(entity: TEntity) {
    return this.httpClient
      .post(`${this.url}`, JSON.stringify(entity))
      .pipe<IAPIResponse<TEntity>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

  public update(entity: TEntity) {
    return this.httpClient
      .put(`${this.url}`, JSON.stringify(entity))
      .pipe<IAPIResponse<TEntity>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

  public delete(id: TKey) {
    return this.httpClient
      .delete(`${this.url}/${id}`)
      .pipe<IAPIResponse<TEntity>>(catchError((err, source) => this.responseHandler.onCatch(err, source)));
  }

}
