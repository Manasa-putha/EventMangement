import { Component, OnInit } from '@angular/core';
import { EventService } from 'src/app/services/event.service';
import { Event, EventResponse, User } from 'src/app/models/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ToasterService } from 'src/app/services/toaster.service';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmDialogComponent } from '../../user/confirm-dialog/confirm-dialog.component';
import { Route, Router } from '@angular/router';
@Component({
  selector: 'app-manage-event',
  templateUrl: './manage-event.component.html',
  styleUrls: ['./manage-event.component.css']
})
export class ManageEventComponent implements OnInit{
  eventForm: FormGroup;
  budgetForm!: FormGroup;
  minDate:Date;
  events: Event[] = [];
  selectedEvent: Event | null = null;
  isAdmin: boolean = false;
  confirmDialogRef!: MatDialogRef<any>;
  currentUserId: number = 0;
  currentUser: User | null = null;
  constructor(private fb: FormBuilder, private eventService: EventService,     private dialog: MatDialog,  private authService: AuthService, private toastr:ToasterService, private router: Router ) {
    this.minDate = new Date(); 
    this.eventForm = this.fb.group({
      name: ['', Validators.required],
      date: ['', Validators.required],
      time: ['', Validators.required],
      location: ['', Validators.required],
      description: ['', Validators.required],
      expenses: [0, Validators.required],
      revenue: [0, Validators.required]
    });

    // this.budgetForm = this.fb.group({
    //   expenses: [0, Validators.required],
    //   revenue: [0, Validators.required]
    // });
  }

  ngOnInit(): void {
    this.authService.userStatus.subscribe(status => {
      this.isAdmin = this.authService.getRoleFromToken() === 'Organizer';
      this.loadEvents();
    });
    this.loadEvents();
    this.checkUserRole();
    this.loadCurrentUser(); 
    
  }

  loadCurrentUser(): void {
    this.currentUser = this.authService.getCurrentUser(); 
    console.log(this.currentUser)// Ensure this method returns the logged-in user
  }
 
  searchBooks(value: string) {
    this.loadEvents();
    value = value.toLowerCase();
  
  }
  getBookCount() {
    let count = 0;
    this.events.forEach((b) => (count += b.id));
    return count;
  }
  checkUserRole(): void {
    const role = localStorage.getItem('role');
    this.authService.userStatus.subscribe(role => {
      
      console.log(role);
      this.isAdmin = role === "Organizer"; // Adjust based on how you define roles
      this.currentUserId = this.authService.getCurrentUserId();
      console.log(role);
    });
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe((response: EventResponse) => {
      const events = response.$values; // Extract the $values property
      console.log(events); // Inspect the structure of the events array
      if (Array.isArray(events)) {
        this.events = events.map(event => ({
          ...event,
          budget: event.budget || { expenses: 0, revenue: 0 },
          isEditing: false,
        //  isRegistered: guestsArray.some(guest => guest.id === this.currentUserId) || false // Check if user is registered
        }));
      } else {
        console.error('Received data is not an array:', events);
      }
      console.log(this.events); // Check the structure of the events array
    });
  }
  
  createOrUpdateEvent(): void {
    if (this.eventForm.valid && this.budgetForm.valid) {
        const event = this.eventForm.value;
        const budget = this.budgetForm.value;

        if (this.selectedEvent) {
            const updatedEvent: Event = { ...this.selectedEvent, ...event, budget };
            this.eventService.updateEvent(updatedEvent).subscribe(() => {
                this.loadEvents();
                this.selectedEvent = null;
            });
        } else {
            const newEvent: Event = { ...event, budget };
            this.eventService.addEvent(newEvent).subscribe(() => {
                this.loadEvents();
            });
        }

        this.eventForm.reset();
        this.budgetForm.reset();
    }
}

  editEvent(event: Event): void {
    if (this.isAdmin) {
      event.isEditing = true;
      this.eventForm.patchValue({
        name: event.eventName,
        date: event.date,
        time: event.time,
        location: event.location,
        description: event.description,
        expenses: event.budget?.expenses || 0,
        revenue: event.budget?.revenue || 0
      });
    }
  }

  saveEvent(event: Event): void {
    if (this.eventForm.valid) {
      const updatedEvent: Event = {
        ...event,
        eventName: this.eventForm.value.name,
        date: this.eventForm.value.date,
        time: this.eventForm.value.time,
        location: this.eventForm.value.location,
        description: this.eventForm.value.description,
        budget: {
          expenses: this.eventForm.value.expenses,
          revenue: this.eventForm.value.revenue
        }
      };
      this.eventService.updateEvent(updatedEvent).subscribe(() => {
        event.isEditing = false;
        this.loadEvents();
      });
    }
  }

  cancelEdit(event: Event): void {
    event.isEditing = false;
    this.eventForm.reset();
  }


  deleteEvent(eventId: number): void {
    if (this.isAdmin) {
      const confirmDialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '250px',
        data: { message: 'Are you sure you want to delete this event?' }
      });

      confirmDialogRef.afterClosed().subscribe(result => {
        if (result === true) {
          this.eventService.deleteEvent(eventId).subscribe(
            () => {
              this.toastr.showSuccess('Event deleted successfully');
              this.loadEvents();
            },
            error => {
              this.toastr.showError('Failed to delete event');
            }
          );
        }
      });
    }
  }
 
  

  updateBudget(): void {
    if (this.budgetForm.valid && this.selectedEvent) {
      const { expenses, revenue } = this.budgetForm.value;
      this.eventService.updateBudget(this.selectedEvent.id, expenses, revenue).subscribe(() => {
        this.loadEvents();
      });
    }
  }
  trackByEventId(index: number, event: Event): number {
    return event.id;
  }
  registerForEvent(eventId: number): void {
    console.log(this.authService.isLoggedIn())
    if (this.authService.isLoggedIn()) {
      const userId = this.authService.getCurrentUser();
      console.log(userId);
    if (userId) {
      // const registerEventDto: User = {
      //   id: currentUser.id,
      //   userName: currentUser.userName,
      //   email: currentUser.email,
      //   mobileNumber: currentUser.mobileNumber,
      //   alternativeNumber: currentUser.alternativeNumber,
      //   password: '', 
      //   role: currentUser.role,
      //   pincode: currentUser.pincode,
      //   city: currentUser.city,
      //   createdAt: currentUser.createdAt,
      //   UpdatedAt: currentUser.UpdatedAt,
      //   tokensAvailable: currentUser.tokensAvailable,
      //   basePrice: currentUser.basePrice
      // };
      const registerEventDto = { userId };

      this.eventService.registerForEvent(eventId, registerEventDto).subscribe(
        () => {
          this.toastr.showSuccess('Registered for event successfully');
          this.router.navigateByUrl('/bookedEvent')
          this.loadEvents(); // Optionally reload events
        },
        error => {
          console.error('Registration error', error); // Log error for debugging
          this.toastr.showError('Failed to register for event');
        }
      );
    } else {
      this.toastr.showError('User not logged in');
    }
  }
  }
}
