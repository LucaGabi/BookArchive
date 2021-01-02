import { Author } from './author.model';
import { AuthorBook } from './authorBook.model';
export class Book {
    public id?: number;
    public title?: string;
    public description?: string;
    public coverImagePath?: string;

    public coverImage?: Blob;
    public clearImage?: boolean

    public authors?: Author[];
    public bookAuthors?: AuthorBook[];

}