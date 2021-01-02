import { BooksService } from './../../books.service';
import { Book } from '@shared/http/books-arch/models/book.model';
import { Component, OnInit } from '@angular/core';
import { subscribeOn } from 'rxjs/operators';


@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.scss']
})

export class BooksListComponent implements OnInit {

  constructor(
    protected vmodel: BooksService,
  ) { }

  ngOnInit(): void {
    this.vmodel.loadBooks().subscribe();
  }

  rem(id: number, i: number) {
    console.log(id);
    if (window.confirm('Do you want to delete?')) {
      this.vmodel.deleteBook(id, i).subscribe();
    }
  }

}