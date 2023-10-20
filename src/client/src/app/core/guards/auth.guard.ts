import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";
import { LocalStorageService } from "../services/local-storage.service";

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router, private localStorage: LocalStorageService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (this.authService.isAuthenticated) {
            if(route.url.length > 0 && route.url[0].path == 'home'){
              return true;
            }
            var branchId = this.localStorage.getItem("branchId");
            var organizationId = this.localStorage.getItem("organizationId");
            if (branchId && organizationId) {
                return true;
            } else {
              console.log(this.router);
                // this.router.navigate(["home"]);
                return true;
            }
        } else {
            this.router.navigate(["login"]);
            return false;
        }
    }
}
