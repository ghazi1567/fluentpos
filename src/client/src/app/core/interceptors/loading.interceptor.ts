import { Injectable } from "@angular/core";
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from "@angular/common/http";
import { Observable } from "rxjs";
import { delay, finalize } from "rxjs/operators";
import { BusyService } from "../services/busy.service";
import { AuthService } from "../services/auth.service";

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    ignorePath: string[] = [""];

    constructor(private busyService: BusyService, private authService: AuthService) {}

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        var pathName = request.url.endsWith(".json") ? "json" : new URL(request.url).pathname;
        if (!this.ignorePath.find((x) => x == pathName) && pathName != "json") {
            var branchId = this.authService.getBranchId;
            var storeId = this.authService.getStoreId;
            if (storeId) {
                request = request.clone({
                    headers: request.headers.set("store-id", storeId)
                });
            }
            if (request.method.toLowerCase() === "post" || request.method.toLowerCase() === "put") {
                if (request.body instanceof FormData) {
                    request = request.clone({
                        body: request.body.append("branchId", branchId)
                    });
                } else {
                    const foo = {};
                    foo["branchId"] = branchId;
                    foo["organizationId"] = this.authService.getOrganizationId;
                    foo["warehouseIds"] = this.authService.getWarehouseIds;
                    // foo["userId"] = this.authService.getUserId;
                    foo["ipAddress"] = this.authService.getIpAddress;
                    // foo["email"] = this.authService.getEmail;
                    request = request.clone({
                        body: { ...(request.body as {}), ...foo }
                    });
                }
            }
            if (request.method.toLowerCase() === "get") {
                if (!request.params.get("branchId")) {
                    request = request.clone({
                        params: request.params.set("branchId", branchId)
                    });
                }

                request = request.clone({
                    params: request.params.set("organizationId", this.authService.getOrganizationId)
                });
                request = request.clone({
                    params: request.params.set("userId", this.authService.getUserId)
                });
                var warehouseIds = this.authService.getWarehouseIds;
                if (warehouseIds) {
                    warehouseIds.forEach((x, index) => {
                        request = request.clone({
                            params: request.params.set("warehouseIds[" + index + "]", x)
                        });
                    });
                } 
            }
        }

        var isFiltering = request.params.get("searchString");
        if (isFiltering === "" || isFiltering === undefined || isFiltering === null) {
            this.busyService.isOverlay.next(true);
        } else {
            this.busyService.isOverlay.next(false);
        }

        this.busyService.isLoading.next(true);
        return next.handle(request).pipe(
            delay(500),
            finalize(() => {
                this.busyService.isLoading.next(false);
            })
        );
    }
}
