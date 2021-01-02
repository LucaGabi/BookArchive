/** Angular core dependencies */
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

/** Third party modules */
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';

/** Custom Components */
import { LoadingPlaceholderComponent } from './loading-placeholder/loading-placeholder.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SpinnerComponent } from './spinner/spinner.component';
import { MaterialModule } from '../material/material.module';

/** Custom Components Registration*/
export const COMPONENTS = [
  LoadingPlaceholderComponent,
  PageNotFoundComponent,
  SpinnerComponent,
];

@NgModule({
  imports: [
    /** Angular core dependencies */
    CommonModule,
    RouterModule,
    /** Third party modules */
    NgbModule,
    TranslateModule,
    MaterialModule
  ],
  declarations: COMPONENTS,
  exports: COMPONENTS
})
export class ComponentsModule { }
