import { BooksService } from './../../books.service';

import { Component, OnInit, NgZone, OnDestroy } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-add-book",
  templateUrl: "./book-add.component.html",
  styleUrls: ["./book-add.component.scss"],
})
export class BooksAddComponent implements OnInit, OnDestroy {


  constructor(
    private router: Router,
    private ngZone: NgZone,
    private route: ActivatedRoute,
    protected vmodel: BooksService,
  ) {
  }
  ngOnDestroy(): void {
    this.vmodel.clear();
  }

  ngOnInit() {
    let id = this.route.snapshot.params.id;
    this.vmodel.loadBookAddEdit(id).subscribe();
  }

  onSubmit() {
    this.vmodel.addUpdateBook()
      .subscribe((r) =>
        !r.hasError && this.ngZone.run(() => this.router.navigateByUrl("/books"))
      );
  }
}
