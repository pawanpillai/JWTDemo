import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class MessageModalService {

	//always keep these variables in-sync with variables in message.component.ts
	//message.component variables - starts
	messageHeader: string = 'Status';	//default value
	messageBody: string = 'Processing completed successfully. Thanks.'	//default value

	showLeftButton: boolean = false;
	showMiddleButton: boolean = false;
	showRightButton: boolean = false;

	leftButtonText: string = '';
	middleButtonText: string = 'Ok';
	rightButtonText: string = '';


	leftButtonClass: string = 'btn-outline-primary';
	middleButtonClass: string = 'btn-outline-primary';
	rightButtonClass: string = 'btn-outline-primary';

	leftButtonRoute: string = '';
	middleButtonRoute: string = '';
	rightButtonRoute: string = '';
	//message.component variables - ends

	public messageModalSubject = new Subject<MessageModalService>();


	constructor() { }

	showOkDefaultMessageAndNavigate(route: string){
		this.showMiddleButton = true;
		this.middleButtonRoute = route;

		this.messageModalSubject.next(this);	//setting the subject and any subscribed observer will get the notification
	}

	showOkCustomMessageAndNavigate(customMessage: string, route: string){
		this.messageBody = customMessage;

		this.showMiddleButton = true;
		this.middleButtonRoute = route;

		this.messageModalSubject.next(this);	//setting the subject and any subscribed observer will get the notification
	}

	showLeftAndRightButtonDefaultMessageAndNavigate(leftRoute: string, rightRoute: string){
		this.showLeftButton = true;
		this.showRightButton = true;

		this.leftButtonText = 'Back';
		this.rightButtonText = 'Add';

		this.leftButtonRoute = leftRoute;
		this.rightButtonRoute = rightRoute;


		this.messageModalSubject.next(this);	//setting the subject and any subscribed observer will get the notification
	}

	showLeftAndRightButtonCustomMessageAndNavigate(customMessage: string, leftRoute: string, rightRoute: string){

		this.messageBody = customMessage;

		this.showLeftButton = true;
		this.showRightButton = true;

		this.leftButtonText = 'Back';
		this.rightButtonText = 'Add';

		this.leftButtonRoute = leftRoute;
		this.rightButtonRoute = rightRoute;


		this.messageModalSubject.next(this);	//setting the subject and any subscribed observer will get the notification
	}

	setMessageContent() {

	}

	reset(){
		this.messageHeader = 'Status';	//default value
		this.messageBody = 'Processing completed successfully. Thanks.'	//default value

		this.showLeftButton = false;
		this.showMiddleButton = false;
		this.showRightButton = false;

		this.leftButtonText = '';
		this.middleButtonText = 'OK';
		this.rightButtonText = '';

		this.leftButtonClass = 'btn-outline-primary';
		this.middleButtonClass = 'btn-outline-primary';
		this.rightButtonClass = 'btn-outline-primary';

		this.leftButtonRoute = '';
		this.middleButtonRoute = '';
		this.rightButtonRoute = '';
		//message.component variables - ends
	}

}
