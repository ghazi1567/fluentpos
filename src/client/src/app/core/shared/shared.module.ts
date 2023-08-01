import { DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ToastrModule } from "ngx-toastr";
import { RouterModule } from "@angular/router";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { ServerErrorComponent } from "./components/server-error/server-error.component";
import { MaterialModule } from "../material/material.module";
import { TableComponent } from "./components/table/table.component";
import { DataPropertyGetterPipe } from "../pipes/data-property-getter.pipe";
import { BrandApiService } from "../api/catalog/brand-api.service";
import { CategoryApiService } from "../api/catalog/category-api.service";
import { ProductApiService } from "../api/catalog/product-api.service";
import { CustomerApiService } from "../api/people/customer-api.service";
import { TranslateModule } from "@ngx-translate/core";
import { AccessDenialComponent } from "./components/access-denial/access-denial.component";
import { HasPermissionDirective } from "../directives/has-permission.directive";
import { HasRoleDirective } from "../directives/has-role.directive";
import { UserApiService } from "../api/identity/user-api.service";
import { RoleApiService } from "../api/identity/role-api.service";
import { IdentityApiService } from "../api/identity/identity-api.service";
import { DeleteDialogComponent } from "./components/delete-dialog/delete-dialog.component";
import { UploaderComponent } from "./components/uploader/uploader.component";
import { HasRightsDirective } from "../directives/has-rights.directive";
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from "@angular/material/core";
import { CustomMomentDateAdapter } from "../helpers/custom-moment-date-adapter";
import { CronGenComponent } from "./components/cron-gen/cron-gen.component";
import { TimePickerComponent } from "./components/cron-gen/time-picker/time-picker.component";
import { AdvanceFilterComponent } from "./components/advance-filter/advance-filter.component";
import { AdvancedSearchComponent } from "./components/advanced-search/advanced-search.component";
import { AgGridModule } from "ag-grid-angular";
import { AgGridBaseComponent } from "./components/ag-grid-base/ag-grid-base.component";
import { GridOverlayComponent } from "./components/ag-grid-base/grid-overlay/grid-overlay.component";
import { GridNorowOverlayComponent } from "./components/ag-grid-base/grid-norow-overlay/grid-norow-overlay.component";
import { ButtonRendererComponent } from './components/ag-grid-base/button-renderer/button-renderer.component';
import { AutoCompleteComponent } from './components/auto-complete/auto-complete.component';

export const MY_FORMATS = {
    parse: {
        dateInput: "LL"
    },
    display: {
        dateInput: "DD-MM-YYYY",
        monthYearLabel: "YYYY",
        dateA11yLabel: "LL",
        monthYearA11yLabel: "YYYY"
    }
};
@NgModule({
    declarations: [
        NotFoundComponent,
        ServerErrorComponent,
        TableComponent,
        DataPropertyGetterPipe,
        AccessDenialComponent,
        HasPermissionDirective,
        HasRoleDirective,
        DeleteDialogComponent,
        UploaderComponent,
        HasRightsDirective,
        CronGenComponent,
        TimePickerComponent,
        AdvanceFilterComponent,
        AdvancedSearchComponent,
        AgGridBaseComponent,
        GridOverlayComponent,
        GridNorowOverlayComponent,
        ButtonRendererComponent,
        AutoCompleteComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        MaterialModule,
        FormsModule,
        TranslateModule,
        AgGridModule,
        ToastrModule.forRoot({
            positionClass: "toast-bottom-right",
            preventDuplicates: true
        })
    ],
    providers: [
        BrandApiService,
        CategoryApiService,
        ProductApiService,
        CustomerApiService,
        IdentityApiService,
        UserApiService,
        RoleApiService,
        { provide: LOCALE_ID, useValue: "en-PK" },
        { provide: MAT_DATE_LOCALE, useValue: "en-PK" },
        { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
        { provide: DateAdapter, useClass: CustomMomentDateAdapter },
        {
            provide: DEFAULT_CURRENCY_CODE,
            useValue: "PKR "
        }
    ],
    exports: [
        ReactiveFormsModule,
        FormsModule,
        TableComponent,
        TranslateModule,
        HasPermissionDirective,
        HasRoleDirective,
        UploaderComponent,
        HasRightsDirective,
        CronGenComponent,
        TimePickerComponent,
        AgGridModule,
        AgGridBaseComponent,
        GridOverlayComponent,
        GridNorowOverlayComponent,
        AutoCompleteComponent
    ]
})
export class SharedModule {}
