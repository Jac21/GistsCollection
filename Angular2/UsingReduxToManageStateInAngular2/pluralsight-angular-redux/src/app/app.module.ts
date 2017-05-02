import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { CourseComponent } from './courses/course.component';
import { CourseService } from './courses/course.service';
import { CourseListComponent } from './courses/course-list.component';

import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryStoryService } from '../api/in-memory-story.service';
import { AppRoutingModule } from './app-routing.module';

import { RouterModule }   from '@angular/router';
import { FilterTextComponent, FilterService } from './blocks/filter-text';
import { ToastComponent, ToastService } from './blocks/toast';
import { SpinnerComponent, SpinnerService } from './blocks/spinner';
import { ModalComponent, ModalService } from './blocks/modal';
import { ExceptionService } from './blocks/exception.service';
import { NgReduxModule, NgRedux } from 'ng2-redux';
import { store, IAppState } from './store';
import { CourseActions } from './courses/course.actions';

// Object.freeze({}) demo
const person = Object.freeze({
  name:"Jeremy",
  surname:"Cantu",
  address: { // IS NOT FROZEN!
    city:"Austin",
    country:"USA"
  }
});

person.address.city = "Somewhere";

// helper function to freeze all objects within
// a parent object that Object.freeze({}) misses
function deepFreeze(o) {
  Object.freeze(o);

  Object.getOwnPropertyNames(o).forEach((prop) => {
    if(
      o.hasOwnProperty(prop)
      && o[prop] != null
      && typeof o[prop] === 'object'
      && !Object.isFrozen(o[prop])) {
        deepFreeze(o[prop]);
      }
  });

  return o;
}

// deepFreeze demo
const frozenPerson = deepFreeze({
  name:"Eskimo",
  surname:"Bob",
  address: { 
    city:"Igloo",
    country:"Antarctica"
  }
});

@NgModule({
  declarations: [
    AppComponent,
    CourseComponent,
    CourseListComponent,
    FilterTextComponent,
    ToastComponent,
    SpinnerComponent,
    ModalComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    InMemoryWebApiModule.forRoot(InMemoryStoryService, { delay: 500 }),
    AppRoutingModule,
    NgReduxModule
  ],
  providers: [
    CourseService,
    FilterService,
    ToastService,
    SpinnerService,
    ModalService,
    ExceptionService,
    CourseActions
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(ngRedux: NgRedux<IAppState>) {
    ngRedux.provideStore(store);
  }
 }
