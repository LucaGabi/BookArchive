import { Injectable } from '@angular/core';
import { of, combineLatest } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { AuthorsApiClient } from '@shared/http/books-arch/authors-api-client.service';
import { BooksApiClient } from '@shared/http/books-arch/books-api-client.service';
import { Author } from '@shared/http/books-arch/models/author.model';
import { Book } from '@shared/http/books-arch/models/book.model';
import { IAPIResponse } from '@shared/http/IAPIResponse';
import { AuthorBook } from '../../shared/http/books-arch/models/authorBook.model';

@Injectable()
export class BooksService {


  constructor(private apiBooks: BooksApiClient,
    private apiAuthors: AuthorsApiClient) {
    this.clear();
  }

  state: {
    book?: Book,
    authorIds?: number[],
    selAuthorIds?: {},
    allBooks?: Book[],
    allAuthors?: Author[]
  } = {};

  clear() {
    this.state = {
      book: {},
      selAuthorIds: {}
    };
  }

  loadBooks() {
    return this.apiBooks.getAll().pipe(
      tap(res => this.state.allBooks = res.data)
    );
  }

  loadAuthors() {
    return this.apiAuthors.getAll().pipe(
      tap(x => this.state.allAuthors = x.data)
    );
  }
  loadBook(id: number) {
    this.state.book.id = id;
    if (this.state.book.id) {
      return this.apiBooks.getOneById(this.state.book.id).pipe(
        tap(x => this.state.book = x.data),
        tap(x => {
          /// for lookup on select option selected html attribute to evaluate to true/false
          this.state.selAuthorIds = x.data?.bookAuthors
            ?.reduce((obj, item) => (obj[item.authorId] = item.bookId, obj), {})
        }),
        tap(x => {
          this.state.authorIds=Object.keys(this.state.selAuthorIds || {}).map(x=>+x);
          // console.log(this.state.selAuthorIds);
          return x;
        }),

      );
    }
    else of({});
  }

  loadBookAddEdit(id: number) {
    return combineLatest(
      this.loadAuthors(),
      this.loadBook(id)
    );
  }

  getSelAuthors(): AuthorBook[] {
    return this.state.authorIds.map(x => <AuthorBook>{ bookId: this.state.book.id, authorId: x });
  }

  setCoverImage(event) {
    if (event?.currentTarget?.files.length) {
      this.state.book.coverImagePath = event.currentTarget.files[0].name;
      this.state.book.coverImage = event.currentTarget.files[0];
    }
  }

  onCreateAdd(r: IAPIResponse<Book>) {
    if (r.hasError) {
      this.errors.server.push('error:' + r.message)
    }
    return of(r)
  }

  addUpdateBook() {
    if (this.isValid()) {
      this.state.book.bookAuthors = this.getSelAuthors();

      if (this.state.book.id) {
        return this.apiBooks.update(this.state.book).pipe(
          tap(x => this.onCreateAdd(x))
        );
      }
      else
        return this.apiBooks.create(this.state.book).pipe(
          tap(x => this.onCreateAdd(x))
        );
    }
    let e: IAPIResponse<Book> = { hasError: true, message: "Invalid form", code: 400 };
    return of(e);
  }


  deleteBook(id: number, i: number) {
    return this.apiBooks.delete(id).pipe(
      tap(res => !res.hasError && this.state.allBooks.splice(i, 1))
    )
  }
  //#region  validation
  errors = {
    title: [],
    description: [],
    coverImagePath: [],
    author: [],
    server: []
  };

  get errorsFlat() {
    return Object.values(this.errors).reduce((p, c) => p = [...p, ...c]);
  }

  get hasMissingAuthor(): boolean {
    return this.state.authorIds.length == 0;
  }

  get hasInvalidTitle(): boolean {
    return !/[A-Za-z ]*/.test(this.state.book.title);
  }

  get hasMissingTitle(): boolean {
    return this.state.book.title?.length === 0;
  }

  get hasInvalidDescription(): boolean {
    return !/[A-Za-z ]*/.test(this.state.book.description);
  }

  get hasMissingDescription(): boolean {
    return this.state.book.description?.length === 0;
  }

  get hasMissingImage(): boolean {
    return this.state.book.coverImagePath?.length === 0;
  }

  validate() {
    this.clearErrors();
    this.hasInvalidTitle &&
      this.errors.title.push('*invalid title');

    this.hasMissingTitle &&
      this.errors.title.push('*missing title');


    this.hasInvalidDescription &&
      this.errors.description.push('*invalid description');


    // this.hasMissingImage &&
    //   this.errors.coverImagePath.push('*missing image');


    this.hasMissingAuthor &&
      this.errors.author.push('*missing author');
  }

  clearErrors() {
    Object.values(this.errors).map(x => x.length = 0);
  }

  isValid(): boolean {
    this.validate();
    return Object.values(this.errors).every(x => x.length == 0);
  }
  //#endregion

}
