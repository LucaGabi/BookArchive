import { RouterModule } from '@angular/router';
/** Angular core dependencies */
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ComponentsModule } from '@shared/components';

/** Custom Containers */
import { LayoutComponent } from './layout/layout.component';
import { MaterialModule } from '../material/material.module';

/** Custom Containers Registration */
const CONTAINERS = [LayoutComponent];

@NgModule({
  imports: [
    /** Angular core dependencies */
    CommonModule,
    ComponentsModule,
    RouterModule,
    MaterialModule
  ],
  declarations: CONTAINERS,
  exports: CONTAINERS
})
export class ContainersModule { }
