import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { LocalStorageService } from "src/app/core/services/local-storage.service";
import { LogoutDialogComponent } from "src/app/core/shared/components/logout-dialog/logout-dialog.component";
import { AuthService } from "../../../core/services/auth.service";

@Component({
    selector: "app-welcome",
    templateUrl: "./welcome.component.html",
    styleUrls: ["./welcome.component.scss"]
})
export class WelcomeComponent implements OnInit {
    fullName: string = "";
    branchName: string = "";
    email: string;
    branchs: any[];
    selectedBranch: any;
    date: Date = new Date();
    isMultipleBranch: boolean = false;
    isNoBranch: boolean = false;
    isSelectedBranch: boolean = false;
    showBranch: boolean = false;
    branchId: any;

    constructor(private authService: AuthService, public dialog: MatDialog, private localStorage: LocalStorageService) {}

    ngOnInit(): void {
        this.getUserDetails();
        setInterval(() => {
            this.date = new Date();
        }, 1000);
    }

    getUserDetails() {
        this.fullName = this.authService.getFullName;
        this.email = this.authService.getEmail;
        let getBranchs = this.authService.getBranchs;
        this.isMultipleBranch = getBranchs.length > 1;
        this.isNoBranch = getBranchs.length == 0;
        this.branchs = JSON.parse(this.localStorage.getItem("branchs"));

        if (!this.isMultipleBranch && !this.isNoBranch) {
           this.updateSelectedBranch(this.branchs[0]);
        } else {
            this.showBranch = true;
        }

        this.selectedBranch = JSON.parse(this.localStorage.getItem("selectedBranch"));
        if (this.selectedBranch) {
            this.branchId = this.selectedBranch.id;
            
            this.isSelectedBranch = true;
            this.showBranch = false;
        }
    }
    onClickLogout() {
        this.authService.logout();

        // const dialogRef = this.dialog.open(LogoutDialogComponent);
        // dialogRef.afterClosed().subscribe((result) => {
        //     if (result) this.authService.logout();
        // });
    }

    onSelectBranch() {
        var branch = this.branchs.find((x) => x.id == this.branchId);
        if (branch) {
           this.updateSelectedBranch(branch);
        }
    }

    onChangeBranch() {
        this.showBranch = true;
    }

    updateSelectedBranch(branch) {
        
        this.localStorage.setItem("selectedBranch", JSON.stringify(branch));
        this.localStorage.setItem("branchId", branch.id);
        this.localStorage.setItem("organizationId", branch.organizationId);
        this.localStorage.setItem("storeId", branch.storeId);
        this.selectedBranch = branch;
        this.isSelectedBranch = true;
        this.showBranch = false;
    }
}
