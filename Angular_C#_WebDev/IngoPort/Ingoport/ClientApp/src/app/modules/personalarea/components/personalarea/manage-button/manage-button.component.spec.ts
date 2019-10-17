import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageButtonComponent } from './manage-button.component';

describe('ManageButtonComponent', () => {
  let component: ManageButtonComponent;
  let fixture: ComponentFixture<ManageButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
