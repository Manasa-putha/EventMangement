import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEventComponent } from './Components/admin/add-event/add-event.component';
import { AllBookedEventsComponent } from './Components/admin/all-booked-events/all-booked-events.component';
import { GuestmanagementComponent } from './Components/admin/guestmanagement/guestmanagement.component';
import { ManageEventComponent } from './Components/admin/manage-event/manage-event.component';
import { LoginComponent } from './Components/login/login.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { RegisterComponent } from './Components/register/register.component';
import { BookedEventsComponent } from './Components/user/booked-events/booked-events.component';
import { adminGuard } from './guards/admin.guard';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'signup',component:RegisterComponent,canActivate: [authGuard,adminGuard]},
  {path:'bookedEvent',component:BookedEventsComponent,canActivate: [authGuard]},
  {path:'addEvent',component:AddEventComponent,canActivate: [authGuard,adminGuard]},
  {path:'manageEvent',component:ManageEventComponent,canActivate: [authGuard]},
  {path:'guestManagement',component:GuestmanagementComponent,canActivate: [authGuard]},
  { path: 'guest-management/:id', component: GuestmanagementComponent,canActivate: [authGuard] },
  {path:'admin/booked-events',component:AllBookedEventsComponent,canActivate:[authGuard]},
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component:PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
