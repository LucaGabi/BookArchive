import { Book } from '@shared/http/books-arch/models/book.model';
import { AuthorBook } from './authorBook.model';
export class Author {

    public id?: number;
    public name?: string;

    public books?: Book[];
    public authorBooks?: AuthorBook[];

}