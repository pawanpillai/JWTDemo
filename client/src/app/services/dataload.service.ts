import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Product } from '../models/product';
import { Vendor } from '../models/vendor';

@Injectable({
	providedIn: 'root'
})
export class DataloadService {

	BASE_URL: string;
	END_POINT: string;

	constructor(private http: HttpClient) {
		this.BASE_URL = environment.apiURL;
	}

	//add methods here to load data
}
