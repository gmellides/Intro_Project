/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserTypeService } from './userType.service';

describe('Service: UserType', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserTypeService]
    });
  });

  it('should ...', inject([UserTypeService], (service: UserTypeService) => {
    expect(service).toBeTruthy();
  }));
});
