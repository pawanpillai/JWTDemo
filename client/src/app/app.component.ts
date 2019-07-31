import { Component, OnInit, OnChanges } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GlobalVariableService } from './services/global-variable.service';
import { MessageModalService } from './services/message-modal.service';
import { AuthenticationService } from './services/authentication.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
	EndPoint: string;
	showMessageModal: boolean = false;
	messageModalServiceData: MessageModalService;

	constructor(
		private globalVariableService: GlobalVariableService,
		private messageModalService: MessageModalService,
		private authenticationService: AuthenticationService) {
	}

	ngOnInit() {
		this.globalVariableService.userType = ''; //default

		this.messageModalService.messageModalSubject
			.subscribe((data: MessageModalService) => {
				this.messageModalServiceData = data;
				this.showMessageModal = true;
			}
			);

	}

	logout(){
		this.authenticationService.logout();
	}

	//reading events emitted by app-message-modal component
	readMessageModalOutputValueEmitted(boolShowHide) {
		this.showMessageModal = boolShowHide;
	}
}
