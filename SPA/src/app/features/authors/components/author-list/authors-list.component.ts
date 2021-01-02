import { AuthorsService } from './../../authors.service';
import { Component, OnInit } from '@angular/core';
import { AuthorsApiClient } from '@shared/http/books-arch/authors-api-client.service';
import { Author } from '@shared/http/books-arch/models/author.model';



@Component({
  selector: 'app-authors-list',
  templateUrl: './authors-list.component.html',
  styleUrls: ['./authors-list.component.scss']
})

export class AuthorsListComponent implements OnInit {

  constructor(
    protected vmodel: AuthorsService,
    private api: AuthorsApiClient,
  ) { }

  ngOnInit(): void {
    this.vmodel.loadAuthors().subscribe();
  }

  rem(id: number, i: number) {

    if (window.confirm('Do you want to delete?')) {
      this.vmodel.deleteAuthor(id, i).subscribe();
    }
  }

}