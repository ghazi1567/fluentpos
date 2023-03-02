import { Component, OnInit } from "@angular/core";
import { ILoadingOverlayAngularComp } from "ag-grid-angular/lib/interfaces";

@Component({
    selector: "app-grid-overlay",
    templateUrl: "./grid-overlay.component.html",
    styleUrls: ["./grid-overlay.component.scss"]
})
export class GridOverlayComponent implements ILoadingOverlayAngularComp {
    params: any;

    agInit(params): void {
        this.params = params;
    }
}
