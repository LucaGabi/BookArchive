import { MediaMatcher } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { sideBarItems } from '../../consts/sidebar-items';
import { NavItem } from '../../models/navbar/nav-item.model';


@Component({
  selector: 'app-layout',
  templateUrl:'./layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnDestroy {

  menu: NavItem [] = sideBarItems;
  mobileQuery: MediaQueryList;

  fillerNav = Array.from({length: 50}, (_, i) => `Nav Item ${i + 1}`);
  private _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  shouldRun = [/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));
}
