import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Event, EventResponse, Guest, User } from '../models/models';
@Injectable({
  providedIn: 'root'
})
export class EventService {
  private events: Event[] = [];
  private eventsSubject = new BehaviorSubject<Event[]>(this.events);
 // private baseUrl :string = 'http://localhost:7256/api/Event';
  private baseUrl: string = 'https://localhost:7256/api/Event';
  userStatus: Subject<string> = new Subject();
  // userStatus: BehaviorSubject<string> = new BehaviorSubject<string>(this.getUserStatus());
   private userPayload:any;
   private jwtHelper = new JwtHelperService();
  events$ = this.eventsSubject.asObservable();
  constructor(private http: HttpClient, private router: Router) {
    this.userPayload = this.decodedToken();
  }
  getToken(){
    return localStorage.getItem('token')
  }
  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }

   
    getEvents() {
      const headers = new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('access_token')}`
      });
      return this.http.get<EventResponse>(this.baseUrl + '/GetEvents', { headers });
    }
    getCurrentUser() : Observable<User>{
      return this.http.get<User>(this.baseUrl+'/Getusers');
    }
    // Fetch a single event by ID from the API
    getEventById(eventId: number): Observable<Event> {
      return this.http.get<Event>(`${this.baseUrl}/${eventId}`);
    }
    // Add a new event to the API
addEvent(newEvent: Event): Observable<Event> {
  return this.http.post<Event>(this.baseUrl + '/CreateEventWithBudget', newEvent);
}

// Update an existing event in the API
updateEvent(updatedEvent: Event): Observable<void> {
  return this.http.put<void>(`${this.baseUrl}/UpdateEvent/${updatedEvent.id}`, updatedEvent);
}
  
 
    // Delete an event by ID from the API
    deleteEvent(eventId: number): Observable<void> {
      return this.http.delete<void>(`${this.baseUrl}/${eventId}`);
    }
  getGuestsForEvent(eventId: number): Observable<Guest[]> {
    return this.http.get<Guest[]>(`${this.baseUrl}/${eventId}/guests`);
  }

  addGuestToEvent(eventId: number, guest: Guest): Observable<Guest> {
    return this.http.post<Guest>(`${this.baseUrl}/${eventId}/guests`, guest);
  }

  deleteGuest(guestId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/guests/${guestId}`);
  }
    // Update the budget for a specific event
    updateBudget(eventId: number, expenses: number, revenue: number): Observable<void> {
      const budget = { expenses, revenue };
      return this.http.put<void>(`${this.baseUrl}/${eventId}/budget`, budget);
    }
 

  registerForEvent(eventId: number, user: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/${eventId}/register`, user);
  }
    // // Get booked events for a user
    // getBookedEvents(userId: number): Observable<Event[]> {
    //   return this.http.get<Event[]>(`${this.baseUrl}/user/${userId}`);
    // }
    getBookedEvents(userId: number): Observable<EventResponse> {
      return this.http.get<EventResponse>(`${this.baseUrl}/user/${userId}`);
    }
    getAllBookedEvents(): Observable<any> {
      return this.http.get(`${this.baseUrl}/admin/bookedEvents`);
    }
}
