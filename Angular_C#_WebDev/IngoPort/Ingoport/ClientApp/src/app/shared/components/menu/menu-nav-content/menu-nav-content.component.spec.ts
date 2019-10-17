import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuNavContentComponent } from './menu-nav-content.component';

describe('MenuNavContentComponent', () => {
  let component: MenuNavContentComponent;
  let fixture: ComponentFixture<MenuNavContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MenuNavContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MenuNavContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
