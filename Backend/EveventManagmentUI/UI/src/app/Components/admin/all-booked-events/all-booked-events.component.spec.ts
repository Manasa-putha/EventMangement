import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllBookedEventsComponent } from './all-booked-events.component';

describe('AllBookedEventsComponent', () => {
  let component: AllBookedEventsComponent;
  let fixture: ComponentFixture<AllBookedEventsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AllBookedEventsComponent]
    });
    fixture = TestBed.createComponent(AllBookedEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
