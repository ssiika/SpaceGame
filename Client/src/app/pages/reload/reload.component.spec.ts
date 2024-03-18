import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReloadComponent } from './reload.component';

describe('ReloadComponent', () => {
  let component: ReloadComponent;
  let fixture: ComponentFixture<ReloadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReloadComponent]
    });
    fixture = TestBed.createComponent(ReloadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
