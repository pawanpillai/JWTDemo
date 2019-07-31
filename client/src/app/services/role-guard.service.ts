import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import decode from 'jwt-decode';

@Injectable({
	providedIn: 'root'
})
export class RoleGuardService implements CanActivate {

	constructor(public auth: AuthenticationService, public router: Router) { }

	canActivate(route: ActivatedRouteSnapshot): boolean {

		// this will be passed from the route config
		// on the data property
		const expectedRole = route.data.expectedRole;
		const access_token = localStorage.getItem('access_token');

		if (access_token != null) {
			// decode the token to get its payload
			const tokenPayload = decode(access_token);

			if (!this.auth.isAuthenticated() || tokenPayload.UserType !== expectedRole) {
				this.auth.logout();	//clear session variables

				if (expectedRole == 'A') {
					this.router.navigate(['/admin/login']);
				}
				else {
					this.router.navigate(['/login']);
				}
				return false;
			}
			return true;
		}
		else {
			this.auth.logout();	//clear session variables
			this.router.navigate(['/login']);
			return false;
		}


	}

}
