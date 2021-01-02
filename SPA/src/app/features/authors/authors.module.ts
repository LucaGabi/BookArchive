import { AuthorsApiClient } from '@shared/http/books-arch/authors-api-client.service';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorsRoutingModule } from './authors-routing.module';

import { PipesModule } from '@shared/pipes';


import { FormsModule } from '@angular/forms';
import { AuthorsAddComponent } from './components/author-add/author-add.component';
import { AuthorsListComponent } from './components/author-list/authors-list.component';
import { AuthorsService } from './authors.service';

@NgModule({
  imports: [CommonModule, AuthorsRoutingModule, FormsModule, PipesModule],
  declarations: [AuthorsListComponent, AuthorsAddComponent],
  providers: [AuthorsApiClient, AuthorsService]
})
export class AuthorsModule { }
