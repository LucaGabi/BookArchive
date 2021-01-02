import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorsAddComponent } from './components/author-add/author-add.component';
import { AuthorsListComponent } from './components/author-list/authors-list.component';


const routes: Routes = [
  { path: '', component: AuthorsListComponent, },
  { path: 'add', component: AuthorsAddComponent, },
  { path: 'edit/:id', component: AuthorsAddComponent, },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorsRoutingModule { }
