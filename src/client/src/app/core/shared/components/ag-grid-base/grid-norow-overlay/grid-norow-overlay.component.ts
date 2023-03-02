import { Component, OnInit } from "@angular/core";
import { ILoadingOverlayAngularComp } from "ag-grid-angular";

@Component({
    selector: "app-grid-norow-overlay",
    templateUrl: "./grid-norow-overlay.component.html",
    styleUrls: ["./grid-norow-overlay.component.scss"]
})
export class GridNorowOverlayComponent implements ILoadingOverlayAngularComp {
    // params: any;

    agInit(params): void {
        //  this.params = params;
    }
}
