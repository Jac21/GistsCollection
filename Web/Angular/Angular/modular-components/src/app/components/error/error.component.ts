import { Component, Input, TemplateRef, ContentChild } from '@angular/core';

@Component({
  selector: 'app-error',
  template: `
    <ng-template [templateOrString]="tplRef || error"
      ><p>{{ error }}</p></ng-template
    >
  `
})
export class ErrorComponent {
  @Input() error: string | TemplateRef<any> = 'There was an error';
  @ContentChild(TemplateRef) tplRef: TemplateRef<any>;
}
