import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { catchError, filter, finalize, switchMap, take } from 'rxjs/operators';

import { AuthenticationService } from './../services/authentication.service';
import { Injectable } from '@angular/core';

@Injectable()
export class HttpErrorFilter implements HttpInterceptor {

    isRefreshingToken = false;
    tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    constructor(private authenticationService: AuthenticationService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((error: any) => {
				if (error.status === 401) {
                    return this.handle401Error(request, next);
                }
                return throwError(error);
            })
        );
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshingToken) {
            this.isRefreshingToken = true;
            this.tokenSubject.next(null);

            return this.authenticationService.refreshToken().pipe(switchMap(token => {
                if (token) {
                    return next.handle(request);
                }
                return of(<any>this.authenticationService.logout());
            }),
            catchError(err => {
                this.authenticationService.logout();
                return throwError(err.error);
            }),
            finalize(() => {
                this.isRefreshingToken = false;
            }));
        } else {
            this.isRefreshingToken = false;

            return this.tokenSubject
                .pipe(filter(token => token != null),
                take(1),
                switchMap(token => {
                    return next.handle(request);
                }));
        }
    }
}
