
export interface User {
  id: number;
  userName: string;
  email: string;
  mobileNumber: string;
  alternativeNumber: string;
  password: string;
  role: Role;
  pincode: number;
  city: string;
  createdAt: string;
  UpdatedAt: string;
  tokensAvailable: number;
  basePrice: number;
}

export enum Role {
  None = "None",
  Organizer = "Organizer",
  Attendee = "Attendee"
}

export interface Event {
  id: number;
  eventName: string;
  date: Date;
  time: string;
  location: string;
  description: string;
  // guests?: User[];
  guests?: Guest[];
  budget: Budget | null;
  isEditing?: boolean;
  isRegistered?: boolean;
}

export interface Guest {
  id: number;
  userName: string;
  contactInfo: string;
  phoneNumber: string;
  eventId: number;
}

export interface Budget {
  expenses: number;
  revenue: number;
}

export interface EventResponse {
  $id: string;
  $values: Event[];
}
