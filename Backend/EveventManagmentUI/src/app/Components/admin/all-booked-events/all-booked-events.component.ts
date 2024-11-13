import { Component, OnInit } from '@angular/core';

import { EventService } from 'src/app/services/event.service';
import { Event } from 'src/app/models/models';
import { ToasterService } from 'src/app/services/toaster.service';

@Component({
  selector: 'app-all-booked-events',
  templateUrl: './all-booked-events.component.html',
  styleUrls: ['./all-booked-events.component.css']
})
export class AllBookedEventsComponent implements OnInit{
  bookedEvents: any[] = [];
 event!: [];
  constructor(
    private eventService: EventService,
    private toastr:ToasterService
  ) {}

  ngOnInit(): void {
    this.eventService.getAllBookedEvents().subscribe(
      (response: any) => {
        this.bookedEvents = response.$values.map( (event: { guests: any; }) => ({
          ...event,
          guests: event.guests ? (event.guests as any).$values : []
        }));
        console.log('All booked events:', this.bookedEvents);
      },
      error => {
        console.error('Failed to load booked events', error);
        this.toastr.showError('Failed to load booked events');
      }
    );
  }
}
