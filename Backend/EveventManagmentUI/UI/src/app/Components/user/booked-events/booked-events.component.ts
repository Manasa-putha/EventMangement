import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { EventService } from 'src/app/services/event.service';
import { ToasterService } from 'src/app/services/toaster.service';
import { Event, EventResponse } from 'src/app/models/models';
@Component({
  selector: 'app-booked-events',
  templateUrl: './booked-events.component.html',
  styleUrls: ['./booked-events.component.css']
})
export class BookedEventsComponent implements OnInit {
  // bookedEvents: Event[] = [];
  bookedEvents: Event[] = [];
  events!: [];
  constructor(
    private eventService: EventService,
    private authService: AuthService,
    private toastr: ToasterService
  ) { }

ngOnInit(): void {
  const userId = this.authService.getCurrentUserId();
  this.eventService.getBookedEvents(userId).subscribe(
    (response: EventResponse) => {
      this.bookedEvents = response.$values.map(event => ({
        ...event,
       // guests: event.guests?.$values || []  // Flattening the guests array
       guests: event.guests ? (event.guests as any).$values : []
      }));
      console.log('Booked events:', this.bookedEvents);  // Verify the data
    },
    error => {
      console.error('Failed to load booked events', error);
      this.toastr.showError('Failed to load booked events');
    }
  );
}
}
