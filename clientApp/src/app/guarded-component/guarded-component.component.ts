import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-guarded-component',
  templateUrl: './guarded-component.component.html',
  styleUrls: ['./guarded-component.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GuardedComponentComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
