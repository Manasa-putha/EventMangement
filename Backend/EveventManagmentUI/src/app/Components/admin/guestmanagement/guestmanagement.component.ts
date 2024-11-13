import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {Event, Guest } from 'src/app/models/models';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-guestmanagement',
  templateUrl: './guestmanagement.component.html',
  styleUrls: ['./guestmanagement.component.css']
})
export class GuestmanagementComponent implements OnInit{
  @Input() eventId!: number; 
  // event!: Event;
  guestForm: FormGroup;
  budgetForm: FormGroup;
  event: Event | undefined;
  guests: Guest[] = [];
  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
     private eventService: EventService,
     private toster:ToastrService) {
    this.guestForm = this.fb.group({
      name: ['', Validators.required],
      contactInfo: ['', Validators.required]
    });
    this.eventId = Number(this.route.snapshot.paramMap.get('id'));
    this.budgetForm = this.fb.group({
      expenses: [0, Validators.required],
      revenue: [0, Validators.required]
    });
  }

  ngOnInit() {
    this.loadGuests();
  }
  loadGuests(): void {
    this.eventService.getGuestsForEvent(this.eventId).subscribe(guests => {
      this.guests = guests;
    });
  }



  addGuest(): void {
    const newGuest: Guest= {
    //  userName: this.guestForm.value,
    //  phoneNumber: this.guestForm.value.phoneNumber,
      //eventID: this.eventId
      id:1,
      userName: '',
      contactInfo: '',
      eventId:1,
       phoneNumber:'',
    };

    this.eventService.addGuestToEvent(this.eventId, newGuest).subscribe(() => {
      this.loadGuests();
      this.toster.success('Guest added successfully!');
      this.guestForm.reset();
    });
  }

  deleteGuest(guestId: number): void {
    this.eventService.deleteGuest(guestId).subscribe(() => {
      this.loadGuests();
    });
  }
}

