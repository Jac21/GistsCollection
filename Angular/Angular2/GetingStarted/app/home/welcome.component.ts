import { Component } from '@angular/core';

@Component({
    moduleId: module.id,
    templateUrl: 'welcome.component.html',
    styleUrls: ['welcome.component.css']
})
export class WelcomeComponent {
    public pageTitle: string = 'Welcome';
}
