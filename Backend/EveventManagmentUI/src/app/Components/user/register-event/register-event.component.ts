import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';

@Component({
  selector: 'app-register-event',
  templateUrl: './register-event.component.html',
  styleUrls: ['./register-event.component.css']
})
export class RegisterEventComponent {
  registerForm: FormGroup;
  eventId: number;

  constructor(private fb: FormBuilder, private eventService: EventService, private route: ActivatedRoute, private router: Router) {
    this.eventId = +this.route.snapshot.paramMap.get('id')!;
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      contactInfo: ['', Validators.required]
    });
  }

  register() {
    // if (this.registerForm.valid) {
    //   const registerEventDto = {
    //     userId: 1, // Replace with actual user ID
    //     ...this.registerForm.value
    //   };
    //   this.eventService.registerForEvent(this.eventId, registerEventDto).subscribe(() => {
    //     this.router.navigate(['/events']);
    //   });
    // }
  }
}
