import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ErrorComponent } from './components/error/error.component';
import { TemplateOrStringDirective } from './directives/template-or-string.directive';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    ErrorComponent,
    TemplateOrStringDirective
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
  public dynamic = 'Text first';

  public renderTpl(tpl) {
    this.dynamic = tpl;
  }
}
