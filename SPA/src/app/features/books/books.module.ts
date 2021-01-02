import { BooksService } from './books.service';
import { BooksApiClient } from '@shared/http/books-arch/books-api-client.service';
import { BooksAddComponent } from './components/book-add/book-add.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BooksRoutingModule } from './books-routing.module';

import { PipesModule } from '@shared/pipes';


import { BooksListComponent } from './components/book-list/books-list.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, BooksRoutingModule, FormsModule, PipesModule],
  declarations: [BooksListComponent, BooksAddComponent],
  providers: [BooksApiClient, BooksService]
})
export class BooksModule { }
