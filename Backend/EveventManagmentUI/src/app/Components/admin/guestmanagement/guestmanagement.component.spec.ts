import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestmanagementComponent } from './guestmanagement.component';

describe('GuestmanagementComponent', () => {
  let component: GuestmanagementComponent;
  let fixture: ComponentFixture<GuestmanagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GuestmanagementComponent]
    });
    fixture = TestBed.createComponent(GuestmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
