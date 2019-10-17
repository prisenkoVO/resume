import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAchievmentItemComponent } from './user-achievment-item.component';

describe('UserAchievmentItemComponent', () => {
  let component: UserAchievmentItemComponent;
  let fixture: ComponentFixture<UserAchievmentItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAchievmentItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAchievmentItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
