import { TestBed } from '@angular/core/testing';

import { MinionUrlService } from './minion-url.service';

describe('MinionUrlService', () => {
  let service: MinionUrlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MinionUrlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
