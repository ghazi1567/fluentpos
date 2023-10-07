import { Component, OnInit } from "@angular/core";
import { ICellRendererAngularComp } from "ag-grid-angular";
import { CustomAction } from "../../table/custom-action";
@Component({
    selector: "app-button-renderer",
    templateUrl: "./button-renderer.component.html",
    styleUrls: ["./button-renderer.component.scss"]
})
export class ButtonRendererComponent implements ICellRendererAngularComp {
    params;
    label: string;
    buttons: any[];
    actionButtons: CustomAction[]
    agInit(params): void {
        this.params = params;
        this.label = this.params.label || null;
        this.actionButtons = this.params.actionButtons || [];
    }

    refresh(params?: any): boolean {
        return true;
    }

    onClick($event) {
        if (this.params.onClick instanceof Function) {
            // put anything into params u want pass into parents component
            const params = {
                button: $event,
                event: this.params.node.data,
                ...this.params
            };
            this.params.onClick(params);
        }
    }
}
