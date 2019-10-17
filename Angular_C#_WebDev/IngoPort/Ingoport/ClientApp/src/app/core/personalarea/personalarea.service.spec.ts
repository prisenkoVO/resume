import { TestBed } from '@angular/core/testing';

import { PersonalareaService } from './personalarea.service';

describe('PersonalareaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PersonalareaService = TestBed.get(PersonalareaService);
    expect(service).toBeTruthy();
  });
});
