import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Vendor } from '../models/vendor';
import { Product } from '../models/product';

@Injectable({
	providedIn: 'root'
})
export class DatasaveService {
	BASE_URL: string;
	END_POINT: string;

	constructor(private http: HttpClient) {
		this.BASE_URL = environment.apiURL;
	}

	//add methods to save data
}
