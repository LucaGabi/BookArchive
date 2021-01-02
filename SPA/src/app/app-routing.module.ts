/** Angular core dependencies */
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        pathMatch:'full',
        redirectTo:'/books'
      },
      {
        path: 'books',
        loadChildren: './features/books/books.module#BooksModule',
      },
      {
        path: 'authors',
        loadChildren: './features/authors/authors.module#AuthorsModule',
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
  
})
export class AppRoutingModule {}
