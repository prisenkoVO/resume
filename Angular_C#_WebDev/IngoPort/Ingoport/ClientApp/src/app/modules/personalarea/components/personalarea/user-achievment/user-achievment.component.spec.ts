import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAchievmentComponent } from './user-achievment.component';

describe('UserAchievmentComponent', () => {
  let component: UserAchievmentComponent;
  let fixture: ComponentFixture<UserAchievmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAchievmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAchievmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
