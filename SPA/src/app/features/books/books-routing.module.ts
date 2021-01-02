import { BooksAddComponent } from './components/book-add/book-add.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BooksListComponent } from './components/book-list/books-list.component';

const routes: Routes = [
  { path: '', component: BooksListComponent, },
  { path: 'add', component: BooksAddComponent, },
  { path: 'edit/:id', component: BooksAddComponent, },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BooksRoutingModule { }
