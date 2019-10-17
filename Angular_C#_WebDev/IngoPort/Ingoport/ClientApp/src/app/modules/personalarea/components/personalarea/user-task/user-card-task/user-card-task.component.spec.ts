import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCardTaskComponent } from './user-card-task.component';

describe('UserCardTaskComponent', () => {
  let component: UserCardTaskComponent;
  let fixture: ComponentFixture<UserCardTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserCardTaskComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserCardTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
