import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { AgGridBaseComponent } from "src/app/core/shared/components/ag-grid-base/ag-grid-base.component";
import { SalaryPerks } from "../../../models/salaryperks";
import { SearchParams } from "../../../models/SearchParams";
import { SalaryPerksService } from "../../../services/salary-perks.service";
import { GlobalPerkFormComponent } from "./global-perk-form/global-perk-form.component";
import * as moment from "moment";
import { SalaryPerksTypeMapping, GlobalPerkTypeMapping } from "src/app/core/enums/SalaryPerksType";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { ToastrService } from "ngx-toastr";
import { DeleteDialogComponent } from "src/app/core/shared/components/delete-dialog/delete-dialog.component";

@Component({
    selector: "app-global-perks",
    templateUrl: "./global-perks.component.html",
    styleUrls: ["./global-perks.component.scss"]
})
export class GlobalPerksComponent implements OnInit {
    perksColumns: any[];
    perksData: SalaryPerks[];
    public SalaryPerksTypeMapping = SalaryPerksTypeMapping;
    public GlobalPerkTypeMapping = GlobalPerkTypeMapping;
    actionButtons: CustomAction[] = [new CustomAction("Remove", "Remove", "Remove", "delete")];

    constructor(private salaryPerksService: SalaryPerksService, public dialog: MatDialog, private toastr: ToastrService) {}

    ngOnInit(): void {
        this.perksData = [];
        this.initColumns();
    }
    initColumns(): void {
        this.perksColumns = [
            { headerName: "Name", field: "name", sortable: true },
            {
                headerName: "Type",
                field: "type",
                sortable: true,
                width: 120,
                valueFormatter: (params) => {
                    let value = params.value;
                    return SalaryPerksTypeMapping[value];
                }
            },
            {
                headerName: "Perk",
                field: "globalPerkType",
                sortable: true,
                width: 120,
                valueFormatter: (params) => {
                    let value = params.value;
                    return GlobalPerkTypeMapping[value];
                }
            },
            { headerName: "Description", field: "description", sortable: true },
            { headerName: "Percentage", field: "percentage", sortable: true, width: 140 },
            { headerName: "Amount", field: "amount", sortable: true, width: 120 },
            {
                headerName: "Effecitve From",
                field: "effecitveFrom",
                sortable: true,
                valueFormatter: (params) => {
                    let value = params.value;
                    let date = moment(value, "YYYY-MM-DD");
                    if (date.isValid()) {
                        value = date.format("YYYY-MM-DD");
                    }
                    return value;
                },
                width: 150
            },
            {
                headerName: "Repeat",
                field: "isRecursion",
                sortable: true,
                width: 120,
                valueFormatter: (params) => {
                    let value = params.value;
                    return value == true ? "Yes" : "No";
                }
            },
            {
                headerName: "UnLimited",
                field: "isRecursionUnLimited",
                sortable: true,
                width: 120,
                valueFormatter: (params) => {
                    let value = params.value;
                    return value == true ? "Yes" : "No";
                }
            },
            {
                headerName: "Ends On",
                field: "recursionEndMonth",
                sortable: true,
                width: 120,
                valueFormatter: (params) => {
                    let value = params.value;
                    if (value) {
                        let date = moment(value, "YYYY-MM-DD");
                        if (date.isValid()) {
                            value = date.format("YYYY-MM-DD");
                        }
                        return value;
                    }

                    return "unlimited";
                }
            },
            {
                headerName: "Edit",
                cellRenderer: "buttonRenderer",
                cellRendererParams: {
                    buttons: ["Increament", "Decrement", "Incentives", "Deductions"],
                    actionButtons: this.actionButtons,
                    onClick: this.onSaveButtonClick.bind(this)
                },
                width: 50,
                pinned: "right"
            }
        ];
    }
    private AgGrid: AgGridBaseComponent;
    @ViewChild("AgGrid") set content(content: AgGridBaseComponent) {
        if (content) {
            // initially setter gets called with undefined
            this.AgGrid = content;
        }
    }
    gridReady(event): void {
        this.getPerks();
    }

    getPerks() {
        let salariesPerksParams = new SearchParams();
        salariesPerksParams.isGlobal = true;
        salariesPerksParams.pageNumber = 0;
        salariesPerksParams.pageSize = 10000;
        this.salaryPerksService.getAll(salariesPerksParams).subscribe((res) => {
            this.perksData = res.data;
        });
    }
    handleReload() {
        this.getPerks();
    }
    openCreateForm(rowData: any = null) {
        const dialogRef = this.dialog.open(GlobalPerkFormComponent, {
            data: rowData
        });
        dialogRef.afterClosed().subscribe((result) => {
            this.getPerks();
        });
    }

    onSaveButtonClick(params) {
        console.log(params);
        if (params.button.key == "Remove") {
            this.openDeleteConfirmationDialog(params.event.id);
        }
    }
    
    openDeleteConfirmationDialog($event: string) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the removal of this perk?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.remove($event);
            }
        });
    }
    remove($event: string): void {
        this.salaryPerksService.delete($event).subscribe(() => {
            this.getPerks();
            this.toastr.info("Perks Removed");
        });
    }
}
