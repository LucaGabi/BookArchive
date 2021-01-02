import { Author } from './../models/author.model';
import { Observable } from "rxjs";
import { Book } from "../models/book.model";
import { IAPIDataService } from '../../IAPIDataService';
import { IAPIResponse } from '../../IAPIResponse';

export interface IBookApiService extends IAPIDataService<Book, Number> {

    getBookAuthors(id: number): Observable<IAPIResponse<Author[]>>;

}