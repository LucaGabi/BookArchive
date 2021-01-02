import { AuthorsService } from './../../authors.service';
import { Component, OnInit, NgZone, OnDestroy } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-add-author",
  templateUrl: "./author-add.component.html",
  styleUrls: ["./author-add.component.scss"],
})
export class AuthorsAddComponent implements OnInit, OnDestroy {

  constructor(
    private router: Router,
    private ngZone: NgZone,
    private route: ActivatedRoute,
    protected vmodel: AuthorsService
  ) {

  }
  ngOnInit() {
    let id = this.route.snapshot.params.id;
    this.vmodel.loadAuthorAddEdit(id).subscribe();
  }
 
  onSubmit() {
    this.vmodel.addUpdateAuthor()
      .subscribe((r) =>
        !r.hasError && this.ngZone.run(() => this.router.navigateByUrl("/authors"))
      );
  }
  ngOnDestroy(): void {
    this.vmodel.clear();
  }
}
