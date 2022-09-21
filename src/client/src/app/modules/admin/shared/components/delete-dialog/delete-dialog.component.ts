import { Component, Inject, Input, OnInit } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

@Component({
    selector: "app-delete-dialog",
    templateUrl: "./delete-dialog.component.html",
    styleUrls: ["./delete-dialog.component.scss"]
})
export class DeleteDialogComponent implements OnInit {
    data: any;
    comments = "";
    constructor(public dialogRef: MatDialogRef<DeleteDialogComponent>, @Inject(MAT_DIALOG_DATA) public _data: any) {
        if (typeof _data == "object") {
            this.data = _data;
        } else {
            this.data = {
                message: _data,
                showComments: false,
                commentLabel: "",
                commentRequired: false,
                confirmButtonLabel: "Confirm",
                cancelButtonLabel: "Cancel",
                confirmColor: "danger"
            };
        }
    }

    ngOnInit(): void {}
    onNoClick(): void {
        this.data.confirmed = false;
        this.data.comments = this.comments;
        this.dialogRef.close(this.data);
    }
    onYesClick(): void {
        this.data.confirmed = true;
        this.data.comments = this.comments;
        this.dialogRef.close(this.data);
    }
}
