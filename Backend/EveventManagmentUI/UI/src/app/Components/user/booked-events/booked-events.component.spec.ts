import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookedEventsComponent } from './booked-events.component';

describe('BookedEventsComponent', () => {
  let component: BookedEventsComponent;
  let fixture: ComponentFixture<BookedEventsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BookedEventsComponent]
    });
    fixture = TestBed.createComponent(BookedEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
