import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EventService } from 'src/app/services/event.service';
import { Event } from 'src/app/models/models';
import { Router } from '@angular/router';
import { ToasterService } from 'src/app/services/toaster.service';
@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent {
  newEventForm: FormGroup;
  minDate!: Date;
  timeInvalid: boolean = false;
  constructor(
   
    private fb: FormBuilder,
    private router:Router,
    private eventService: EventService,
    private toastr:ToasterService) {
    this.minDate = new Date(); 
    this.newEventForm = this.fb.group({
      eventName: ['', Validators.required],
      date: ['', Validators.required],
      time: ['', Validators.required],
      location: ['', Validators.required],
      description: ['',Validators.required],
      expenses: ['',Validators.required],
      revenue: ['',Validators.required],
    });
  }

 
  addNewEvent() {
    if (this.newEventForm.valid) {
      const newEvent: Event = {
        ...this.newEventForm.value,
        userID: 1, 
        guests: [],
        budget: {
          expenses: this.newEventForm.value.expenses,
          revenue: this.newEventForm.value.revenue
        }
      };
      this.eventService.addEvent(newEvent).subscribe(createdEvent => {
        console.log('Event created:', createdEvent);
        this.toastr.showSuccess("Event created sucessfully");
        this.newEventForm.reset();
        this.router.navigateByUrl("/manageEvent");
      },
      error => {
        this.toastr.showError('Failed to add event',error);
      }
      );
    }
  }
  // convertTimeToAmPm() {
  //   const timeControl = this.newEventForm.get('time');
  //   if (timeControl) {
  //     const timeValue = timeControl.value;
  //     const [hours, minutes] = timeValue.split(':').map(Number);
  //     const ampm = hours >= 12 ? 'PM' : 'AM';
  //     const formattedHours = hours % 12 || 12;
  //     const formattedTime = `${formattedHours}:${minutes.toString().padStart(2, '0')} ${ampm}`;
  //     timeControl.setValue(formattedTime);
  //   }
  timeValidator(control: AbstractControl): { [key: string]: any } | null {
    const timeRegEx = /^(0?[1-9]|1[0-2]):[0-5][0-9] (AM|PM)$/i;
    if (control.value && !timeRegEx.test(control.value)) {
      return { invalidTime: true };
    }
    return null;
  }

  onTimeInput() {
    const timeControl = this.newEventForm.get('time');
    if (timeControl && timeControl.invalid && timeControl.touched) {
      this.timeInvalid = true;
    } else {
      this.timeInvalid = false;
    }
  }
  
}

