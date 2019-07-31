import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable({
	providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

	constructor(public auth: AuthenticationService, public router: Router) { }

	canActivate(): boolean {

		//this.router.url != '/register' condition is used to ignore route guard to enable step 2 of registration
		if (!this.auth.isAuthenticated() && this.router.url != '/register') {
			this.auth.logout();	//clear session variables
			this.router.navigate(['/']);
			return false;
		}
		return true;
	}

}
