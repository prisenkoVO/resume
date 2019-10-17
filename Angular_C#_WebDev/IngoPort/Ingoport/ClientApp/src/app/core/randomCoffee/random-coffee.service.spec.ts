import { TestBed } from '@angular/core/testing';

import { RandomCoffeeService } from './random-coffee.service';

describe('RandomCoffeeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RandomCoffeeService = TestBed.get(RandomCoffeeService);
    expect(service).toBeTruthy();
  });
});
