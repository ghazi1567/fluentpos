import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { TableColumn } from "./table-column";
import { MatSort, Sort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { DeleteDialogComponent } from "src/app/modules/admin/shared/components/delete-dialog/delete-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PageEvent } from "@angular/material/paginator";
import { CustomAction } from "./custom-action";
import { AdvanceFilterComponent } from "../advance-filter/advance-filter.component";
import { AdvanceFilter } from "src/app/core/models/Filters/AdvanceFilter";
import { AdvancedSearchComponent } from "../advanced-search/advanced-search.component";
import { NgAsConfig, NgAsSearchTerm } from "src/app/core/models/Filters/SearchTerm";

@Component({
    selector: "app-table",
    templateUrl: "./table.component.html",
    styleUrls: ["./table.component.scss"]
})
export class TableComponent implements OnInit, AfterViewInit {
    public tableDataSource = new MatTableDataSource([]);
    public displayedColumns: string[];
    public displayedButtons: string[];
    @Input() customActionOneData: CustomAction;
    @Input() customActionButtons: CustomAction[];

    @Input() customActionData: CustomAction;
    searchString: string;
    @Input() totalCount: number;
    @Input() pageSize: number;
    @Output() onPageChanged = new EventEmitter<PaginatedFilter>();

    @ViewChild(MatSort, { static: true }) matSort: MatSort;

    @Input() title: string;
    @Input() subtitle: string;

    @Input() isSortable = false;
    @Input() columns: TableColumn[];

    @Input() set data(data: any[]) {
        this.setTableDataSource(data);
    }
    @Input() permissions: any[] = [];
    @Input() moduleName: string = "";

    @Output() onFilter: EventEmitter<string> = new EventEmitter<string>();
    @Output() onReload: EventEmitter<any> = new EventEmitter<any>();
    @Output() onSort: EventEmitter<Sort> = new EventEmitter<Sort>();
    @Output() onCustomActionOne: EventEmitter<any> = new EventEmitter();
    @Output() onCustomActionButton: EventEmitter<any> = new EventEmitter();
    @Output() onCustomAction: EventEmitter<any> = new EventEmitter();
    @Output() onCreateForm: EventEmitter<any> = new EventEmitter();
    @Output() onEditForm: EventEmitter<any> = new EventEmitter();
    @Output() onView: EventEmitter<any> = new EventEmitter();
    @Output() onDelete: EventEmitter<string> = new EventEmitter<string>();
    @Output() onExport: EventEmitter<string> = new EventEmitter<string>();
    @Output() onImport: EventEmitter<string> = new EventEmitter<string>();
    @Input() advanceFilters: NgAsConfig;
    @Output() onAdvanceFilters: EventEmitter<NgAsSearchTerm> = new EventEmitter<NgAsSearchTerm>();

    constructor(public dialog: MatDialog) {}
    gold: EventEmitter<{ data: CustomAction }>[];
    ngOnInit(): void {
        const columnNames = this.columns.map((tableColumn: TableColumn) => tableColumn.name);
        this.displayedColumns = columnNames;

        const actionColumn = this.columns.find((x) => x.dataKey === "action");
        if (actionColumn && actionColumn.buttons && actionColumn.buttons.length > 0) {
            this.displayedButtons = actionColumn.buttons;
        } else {
            this.displayedButtons = ["View", "Update", "Remove", "Register"];
        }
    }

    ngAfterViewInit(): void {
        this.tableDataSource.sort = this.matSort;
    }

    setTableDataSource(data: any) {
        this.tableDataSource = new MatTableDataSource<any>(data);
    }
    openCustomActionOne($event: any) {
        this.onCustomActionOne.emit($event);
    }
    openCustomActionButton($event: any, button: CustomAction) {
        this.onCustomActionButton.emit({
            event: $event,
            button: button
        });
    }
    handleCustomAction() {
        this.onCustomAction.emit(this.tableDataSource.data);
    }

    openCreateForm() {
        this.onCreateForm.emit();
    }

    openEditForm($event?) {
        this.onEditForm.emit($event);
    }
    openViewForm($event?) {
        this.onView.emit($event);
    }

    handleReload() {
        this.searchString = "";
        this.onReload.emit();
    }

    handleFilter() {
        this.onFilter.emit(this.searchString);
    }
    handleExport() {
        this.onExport.emit("");
    }
    handleImport() {
        this.onImport.emit("");
    }

    handleSort(sortParams: Sort) {
        sortParams.active = this.columns.find((column) => column.name === sortParams.active).dataKey;
        if (sortParams.direction == "") {
            sortParams.direction = "asc";
        }
        this.onSort.emit(sortParams);
    }

    openDeleteConfirmationDialog($event: string) {
        const dialogRef = this.dialog.open(DeleteDialogComponent, {
            data: "Do you confirm the removal of this record?"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result.confirmed) {
                this.onDelete.emit($event);
            }
        });
    }
    onPageChange(pageEvent: PageEvent) {
        const event: PaginatedFilter = {
            pageNumber: pageEvent.pageIndex + 1 ?? 1,
            pageSize: pageEvent.pageSize ?? 10
        };
        this.onPageChanged.emit(event);
    }

    isAllSelected() {
        var result = true;
        this.tableDataSource.data.forEach((element) => {
            if (element.selected === false) result = false;
        });
        return result;
    }

    toggleTableDataSourceChecking(condition: boolean) {
        this.tableDataSource.data.forEach((element) => {
            element.selected = condition;
        });
    }

    masterToggle() {
        console.log(this.isAllSelected());
        this.isAllSelected() ? this.toggleTableDataSourceChecking(false) : this.toggleTableDataSourceChecking(true);
    }

    isDisplayButton(button: string, row: any) {
        if (row && row[button] && row[button] == true) {
            return false;
        }
        var btn = this.displayedButtons.find((x) => x == button);
        return btn != null;
    }
    searchIcon = "filter_list";
    openAdvanceFilter() {
        const dialogRef = this.dialog.open(AdvancedSearchComponent, {
            data: this.advanceFilters
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.searchIcon = "grading";
            } else {
                this.searchIcon = "filter_list";
            }
            this.onAdvanceFilters.emit(result);
        });
    }
}
