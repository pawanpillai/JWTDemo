import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { tap, finalize } from 'rxjs/operators';

@Injectable()
export class HttpFilter implements HttpInterceptor {

    private apiUrl = environment.apiURL;
	count = 0;

	constructor(private spinner: NgxSpinnerService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		this.spinner.show()
		this.count++;

		return next.handle(this.addAuthentication(request))
			.pipe(tap(
				event => {
					//console.log(event)
				},
				error => {
					//console.log(error)
				}
			), finalize(() => {
				this.count--;
				if (this.count == 0) this.spinner.hide()
			})
		);
    }

    private addAuthentication(request: HttpRequest<any>): HttpRequest<any> {

            const token =  localStorage.getItem('access_token');
            if (token) {
                request = request.clone({
                    setHeaders: {Authorization: 'Bearer ' + token}
                });
            }

        return request;

    }

}
