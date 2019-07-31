//This service maintains all the global variables shared across the components
import { Injectable } from '@angular/core';

@Injectable()
export class GlobalVariableService {
	userType: string = '';
	loggedIn: boolean = false;

	constructor() { }

}
