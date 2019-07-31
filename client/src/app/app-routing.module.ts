import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { AuthGuardService } from './services/auth-guard.service';
import { RoleGuardService } from './services/role-guard.service';


export const routes: Routes = [
	{ path: '', component: VendorLoginComponent },
	{ path: 'register', component: RegisterComponent },
	{ path: 'vendor/login', component: VendorLoginComponent },
	{ path: 'admin/login', component: AdminLoginComponent },
	{ path: 'admin/options', component: AdminOptionsComponent, canActivate: [AuthGuardService, RoleGuardService], data: { expectedRole: 'A' } },
	{ path: 'vendor/options', component: VendorOptionsComponent, canActivate: [AuthGuardService, RoleGuardService], data: { expectedRole: 'V' } },
	{ path: 'vendor/profile/:vendorId', component: VendorProfileComponent, canActivate: [AuthGuardService] },
	{ path: 'redirect', children: [{ path: "**", component: RouteRedirectComponent }] },	//handling special redirect paths in the form of /redirect/path1/path2 >> will redurect to /path1/path2
	{ path: '**', redirectTo: '', pathMatch: 'full' }


	//format of route object
	//{
	//    path: 'somepath',
	//    component: SomeComponent,
	//    resolve: SomeResolver,
	//    canActivate: SomeService
	//}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
