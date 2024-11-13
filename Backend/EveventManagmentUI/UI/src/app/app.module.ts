import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { PageHeaderComponent } from './Components/page-header/page-header.component';
import { PageFooterComponent } from './Components/page-footer/page-footer.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { PageSideNavComponent } from './Components/page-side-nav/page-side-nav.component';
import { MaterialModule } from './material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { provideToastr, ToastrModule } from 'ngx-toastr';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AddEventComponent } from './Components/admin/add-event/add-event.component';
import { ManageEventComponent } from './Components/admin/manage-event/manage-event.component';
import { GuestmanagementComponent } from './Components/admin/guestmanagement/guestmanagement.component';
import { RegisterEventComponent } from './Components/user/register-event/register-event.component';
import { ConfirmDialogComponent } from './Components/user/confirm-dialog/confirm-dialog.component';
import { BookedEventsComponent } from './Components/user/booked-events/booked-events.component';
import { AllBookedEventsComponent } from './Components/admin/all-booked-events/all-booked-events.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    PageHeaderComponent,
    PageFooterComponent,
    PageNotFoundComponent,
    PageSideNavComponent,
    AddEventComponent,
    ManageEventComponent,
    GuestmanagementComponent,
    RegisterEventComponent,
    ConfirmDialogComponent,
    BookedEventsComponent,
    AllBookedEventsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule, 
    HttpClientModule,
    ToastrModule.forRoot({ positionClass: 'inline' }),
  ],
  providers: [
    provideAnimations(), 

    provideToastr({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),{
    provide:HTTP_INTERCEPTORS,
    useClass:TokenInterceptor,
    multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
