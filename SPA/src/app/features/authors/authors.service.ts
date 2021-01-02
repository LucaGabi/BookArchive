import { BooksApiClient } from '@shared/http/books-arch/books-api-client.service';
import { Injectable } from '@angular/core';
import { combineLatest, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthorsApiClient } from '@shared/http/books-arch/authors-api-client.service';
import { Author } from '@shared/http/books-arch/models/author.model';
import { AuthorBook } from '@shared/http/books-arch/models/authorBook.model';
import { Book } from '@shared/http/books-arch/models/book.model';
import { IAPIResponse } from '@shared/http/IAPIResponse';

@Injectable({
    providedIn: 'root'
})
export class AuthorsService {

    state: {
        allAuthors?: Author[];
        selBookIds?: {};
        bookIds?: number[];
        allBooks?: Book[];
        author?: Author;
    }


    constructor(
        private apiAuthor: AuthorsApiClient,
        private apiBooks: BooksApiClient,
    ) {
        this.clear();
    }
    clear() {
        this.state = {
            author: {},
            bookIds: [],
            selBookIds: {},
        }
    }

    loadBooks() {
        return this.apiBooks.getAll().pipe(
            tap(x => { this.state.allBooks = x.data; return x; })
        );
    }

    loadAuthors() {
        return this.apiAuthor.getAll().pipe(
            tap(res => this.state.allAuthors = res.data)
        )
    }

    loadAuthor(id: number) {
        this.state.author.id = id;
        if (this.state.author.id) {
            return this.apiAuthor.getOneById(this.state.author.id).pipe(
                tap(x => this.state.author = x.data),
                tap(x => {
                    /// for lookup on select option selected html attribute to evaluate to true/false
                    this.state.selBookIds = x.data?.authorBooks
                        ?.reduce((obj, item) => (obj[item.bookId] = item.authorId, obj), {})
                }),
                tap(x => {
                    this.state.bookIds = Object.keys(this.state.selBookIds || {}).map(x => +x);
                    // console.log(this.state.selAuthorIds);
                    return x;
                }),
            );
        }
        return of(null);
    }

    getSelBooks(): AuthorBook[] {
        return this.state.bookIds
            .map(x => <AuthorBook>{ bookId: x, authorId: this.state.author.id });
    }

    loadAuthorAddEdit(id: any) {
        return combineLatest(
            this.loadBooks(),
            this.loadAuthor(id),
        );
    }

    addUpdateAuthor() {
        if (this.isValid()) {
            this.state.author.authorBooks = this.getSelBooks();
            if (this.state.author.id) {
                return this.apiAuthor.update(this.state.author).pipe(
                    tap(x => this.onCreateAdd(x))
                )

            }
            else
                return this.apiAuthor.create(this.state.author).pipe(
                    tap(x => this.onCreateAdd(x))
                )
        }
        let e: IAPIResponse<Author> = { hasError: true, message: "Invalid form", code: 400 };
        return of(e);
    }

    onCreateAdd(r: IAPIResponse<Author>) {
        if (r.hasError) {
            this.errors.server.push('error:' + r.message)
        }
        return of(r)
    }

    deleteAuthor(id: number, i: number) {
        return this.apiAuthor.delete(id).pipe(
            tap(res => !res.hasError && this.state.allAuthors.splice(i, 1))
        );
    }



    //#region  validation
    errors = {
        name: [],
        authorBooks: [],
        server: []
    };

    get errorsFlat() {
        return Object.values(this.errors).reduce((p, c) => p = [...p, ...c]);
    }


    get hasInvalidName(): boolean {
        return !/[A-Za-z ]*/.test(this.state.author.name);
    }

    get hasMissingName(): boolean {
        return this.state.author.name?.length === 0;
    }


    validate() {
        this.clearErrors();
        this.hasInvalidName &&
            this.errors.name.push('*invalid name');

        this.hasMissingName &&
            this.errors.name.push('*missing name');
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
