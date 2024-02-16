import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialog } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { Branch } from "src/app/modules/admin/org/models/branch";
import { SearchParams } from "src/app/modules/admin/org/models/SearchParams";
import { BranchService } from "src/app/modules/admin/org/services/branch.service";
import { User } from "../../../models/user";
import { UserBranchModel } from "../../../models/userBranch";
import { UserRoleModel } from "../../../models/userRole";
import { UserService } from "../../../services/user.service";

@Component({
    selector: "app-user-branch-form",
    templateUrl: "./user-branch-form.component.html",
    styleUrls: ["./user-branch-form.component.scss"]
})
export class UserBranchFormComponent implements OnInit {
    userBranchs: UserBranchModel[];
    userBranchColumns: TableColumn[];
    searchString: string;
    userRoleActionData: CustomAction = new CustomAction("Update User Branchs", "update", "primary");
    branchs: PaginatedResult<Branch>;

    constructor(@Inject(MAT_DIALOG_DATA) public data: User, private dialogRef: MatDialog, public userService: UserService, public branchService: BranchService, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getBranchs();
        this.initColumns();
    }

    getUsers(): void {
        this.userService.getUserBranchs(this.data.id).subscribe((result) => {
            let userBranchs = result.data.userBranchs;
            let allBranchs: UserBranchModel[] = [];
            this.branchs.data.forEach((b) => {
                var branch = userBranchs.find((x) => x.branchId == b.id);
                var ub: UserBranchModel = {
                    branchId: b.id,
                    organizationId: b.organizationId,
                    identityUserId: this.data.id,
                    branchName: b.name
                } as UserBranchModel;
                if (branch) {
                    ub.active = branch.active;
                    ub.selected = branch.selected;
                } else {
                    ub.active = false;
                    ub.selected = false;
                }
                allBranchs.push(ub);
            });
            this.userBranchs = allBranchs;
        });
    }
    getBranchs(): void {
        var branchParams = new SearchParams();
        this.branchService.getAlls(branchParams).subscribe((result) => {
            this.branchs = result;
            this.getUsers();
        });
    }

    initColumns(): void {
        this.userBranchColumns = [
            { name: "Branch Id", dataKey: "branchId", isSortable: true, isShowable: true },
            { name: "Branch Name", dataKey: "branchName", isSortable: true, isShowable: true },
            { name: "Selected", dataKey: "selected", isSortable: true, isShowable: true }
        ];
    }

    submitUserBranchs(): void {
        this.userService.updateUserBranchs(this.data.id, { userBranchs: this.userBranchs }).subscribe((result) => {
            this.toastr.success(result.messages[0]);
            this.dialogRef.closeAll();
        });
    }
}
