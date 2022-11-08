import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { PaginatedFilter } from "src/app/core/models/Filters/PaginatedFilter";
import { PaginatedResult } from "src/app/core/models/wrappers/PaginatedResult";
import { User } from "../../models/user";
import { UserParams } from "../../models/userParams";
import { UserService } from "../../services/user.service";
import { UserFormComponent } from "./user-form/user-form.component";
import { ToastrService } from "ngx-toastr";
import { Sort } from "@angular/material/sort";
import { TableColumn } from "src/app/core/shared/components/table/table-column";
import { CustomAction } from "src/app/core/shared/components/table/custom-action";
import { UserRoleFormComponent } from "./user-role-form/user-role-form.component";
import { UserBranchFormComponent } from "./user-branch-form/user-branch-form.component";

@Component({
    selector: "app-user",
    templateUrl: "./user.component.html",
    styleUrls: ["./user.component.scss"]
})
export class UserComponent implements OnInit {
    users: PaginatedResult<User>;
    userColumns: TableColumn[];
    userParams = new UserParams();
    searchString: string;
    userRoleActionData: CustomAction = new CustomAction("Manage User Roles", "user_role",'Edit');
    actionButtons: CustomAction[] = [new CustomAction("Manage User Roles", "user_role",'Edit'), new CustomAction("Manage User Branchs", "user_branch",'Edit')];
    constructor(public userService: UserService, public dialog: MatDialog, public toastr: ToastrService) {}

    ngOnInit(): void {
        this.getUsers();
        this.initColumns();
    }

    getUsers(): void {
        this.userService.getUsers(this.userParams).subscribe((result) => {
            this.users = result;
        });
    }

    initColumns(): void {
        this.userColumns = [
            { name: "Id", dataKey: "id", isSortable: true, isShowable: true },
            { name: "UserName", dataKey: "userName", isSortable: true, isShowable: true },
            { name: "FirstName", dataKey: "firstName", isSortable: true, isShowable: true },
            { name: "LastName", dataKey: "lastName", isSortable: true, isShowable: true },
            { name: "Email", dataKey: "email", isSortable: true, isShowable: true },
            { name: "IsActive", dataKey: "isActive", isSortable: true, isShowable: true },
            { name: "EmailConfirmed", dataKey: "emailConfirmed", isSortable: true, isShowable: true },
            { name: "PhoneNumber", dataKey: "phoneNumber", isSortable: true, isShowable: true },
            { name: "Action", dataKey: "action", position: "right", buttons:['Register','Update','Remove','user_branch','user_role'] }
        ];
    }

    pageChanged(event: PaginatedFilter): void {
        this.userParams.pageNumber = event.pageNumber;
        this.userParams.pageSize = event.pageSize;
        this.getUsers();
    }

    openForm(user?: User): void {
        const dialogRef = this.dialog.open(UserFormComponent, {
            data: user
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getUsers();
            }
        });
    }

    remove($event: string): void {
        this.toastr.info("You can not remove user.");

        // this.userService.deleteUser($event).subscribe(() => {
        //   this.getUsers();
        //   this.toastr.info('User Removed');
        // });
    }

    sort($event: Sort): void {
        this.userParams.orderBy = $event.active + " " + $event.direction;
        console.log(this.userParams.orderBy);
        this.getUsers();
    }

    filter($event: string): void {
        this.userParams.searchString = $event.trim().toLocaleLowerCase();
        this.userParams.pageNumber = 0;
        this.userParams.pageSize = 0;
        this.getUsers();
    }

    reload(): void {
        this.userParams.searchString = "";
        this.userParams.pageNumber = 0;
        this.userParams.pageSize = 0;
        this.getUsers();
    }

    openUserRolesForm(user: User): void {
        const dialogRef = this.dialog.open(UserRoleFormComponent, {
            data: user,
            panelClass: "mat-dialog-container-no-padding"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getUsers();
            }
        });
    }
    openUserBranchsForm(user: User): void {
        const dialogRef = this.dialog.open(UserBranchFormComponent, {
            data: user,
            panelClass: "mat-dialog-container-no-padding"
        });
        dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                this.getUsers();
            }
        });
    }
    openCustomActionButton(data: any): void {
        if (data.button.key == "user_role") {
            this.openUserRolesForm(data.event);
        }
        else if (data.button.key == "user_branch") {
            this.openUserBranchsForm(data.event);
        }
    }
}
