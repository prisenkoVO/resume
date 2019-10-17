import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeammateCardComponent } from './teammate-card.component';

describe('TeammateCardComponent', () => {
  let component: TeammateCardComponent;
  let fixture: ComponentFixture<TeammateCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeammateCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeammateCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
