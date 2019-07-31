//angular
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TagInputModule } from 'ngx-chips';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxMaskModule } from 'ngx-mask';
import { NgxCurrencyModule } from "ngx-currency";


//services
import { GlobalVariableService } from './services/global-variable.service';
import { MessageModalService } from './services/message-modal.service';

//components
import { HttpErrorFilter } from './common/http-error-filter';
import { HttpFilter } from './common/http-filter';
import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';




@NgModule({
	declarations: [
		AppComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		ReactiveFormsModule,
		AppRoutingModule,
		RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' }),
		NgxSpinnerModule,
		BrowserAnimationsModule,
		TagInputModule,
		AgGridModule.withComponents([]),
		ModalModule.forRoot(),
		NgxMaskModule.forRoot(),
		NgxCurrencyModule
	],
	providers: [
		GlobalVariableService,
		MessageModalService,
		DatePipe,
		{ provide: HTTP_INTERCEPTORS, useClass: HttpErrorFilter, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: HttpFilter, multi: true }

	],
	entryComponents: [
		ProductAgGridLinkRendererComponent,
		VendorAgGridLinkRendererComponent,
		OrderAgGridLinkRendererComponent
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
