import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import * as moment from 'moment';

import { Vendor } from '../models/vendor';
import { GlobalVariableService } from './global-variable.service';
import { Router, ActivatedRoute } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
	private currentUserSubject: BehaviorSubject<Vendor>;
	public currentUser: Observable<Vendor>;
	BaseUrl: string;
	EndPoint: string;

	constructor(private http: HttpClient,
		private globalVariableService: GlobalVariableService,
		private router: Router) {
		this.BaseUrl = environment.apiURL;
		this.currentUserSubject = new BehaviorSubject<Vendor>(JSON.parse(localStorage.getItem('currentUser')));
		this.currentUser = this.currentUserSubject.asObservable();
	}

	public get currentUserValue(): Vendor {
		return this.currentUserSubject.value;
	}

	login(emailaddress: string, password: string, usertype: string) {
		localStorage.setItem('base_url', this.BaseUrl);
		localStorage.setItem('client_id', "100");
		localStorage.setItem('client_secret', "0000");
		localStorage.setItem('username', emailaddress);
		localStorage.setItem('usertype', usertype);

		this.EndPoint = '/auth';
		let request = {
			"grant_type": "password",
			"client_id": "100",
			"client_secret": "0000",
			"username": emailaddress,
			"password": password,
			"usertype": usertype
		};

		return this.http.post<any>(this.BaseUrl + this.EndPoint, request)
			.pipe(map(authResult => {
				//console.log(authResult);
				this.setSession(authResult);
				return authResult;
			}));
	}

	logout() {
		this.globalVariableService.loggedIn = false;

		// remove user from local storage to log user out
		localStorage.removeItem('currentUser');
		localStorage.removeItem('access_token');
		localStorage.removeItem("expires_at");
		localStorage.removeItem('refresh_token');
		localStorage.removeItem('client_id');
		localStorage.removeItem('client_secret');
		localStorage.removeItem('base_url');
		localStorage.removeItem('username');
		localStorage.removeItem('usertype');

		this.currentUserSubject.next(null);

		this.router.navigate(['/']);
	}

	private setSession(authResult) {
		this.globalVariableService.loggedIn = true;
		let authData = JSON.parse(authResult.data);
		//console.log(authData);

		const expiresAt = moment().add(authData.expires_in, 'second');

		localStorage.setItem('access_token', authData.access_token);
		localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
		localStorage.setItem('refresh_token', authData.refresh_token);


	}

	refreshToken() {
		this.EndPoint = '/auth';

		let client_id = localStorage.getItem("client_id");
		let client_secret = localStorage.getItem("client_secret");
		let refresh_token = localStorage.getItem("refresh_token");
		let username = localStorage.getItem("username");
		let usertype = localStorage.getItem("usertype");


		let request = {
			"grant_type": "refresh_token",
			"client_id": client_id,
			"client_secret": client_secret,
			"username": username,
			"refresh_token": refresh_token,
			"usertype": usertype
		};

		return this.http.post<any>(this.BaseUrl + this.EndPoint, request)
			.pipe(map(authResult => {
				//console.log(authResult);
				this.setSession(authResult);
				return authResult;
			}));
	}

	isAuthenticated(): boolean {

		const refresh_token = localStorage.getItem("refresh_token");

		// Check whether refresh_token exists or not
		// true or false
		return (refresh_token != null);
	}
}
