/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserTitleService } from './userTitle.service';

describe('Service: UserTitle', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserTitleService]
    });
  });

  it('should ...', inject([UserTitleService], (service: UserTitleService) => {
    expect(service).toBeTruthy();
  }));
});
