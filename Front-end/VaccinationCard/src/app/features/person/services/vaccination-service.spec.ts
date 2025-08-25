import { TestBed } from '@angular/core/testing';

import { VaccinationService } from './vaccination-service';

describe('VaccinationService', () => {
  let service: VaccinationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VaccinationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
