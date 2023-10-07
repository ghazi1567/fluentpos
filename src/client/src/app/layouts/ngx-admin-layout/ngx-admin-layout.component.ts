import { Component, OnInit } from "@angular/core";
import { NbMenuItem } from "@nebular/theme";

@Component({
    selector: "app-ngx-admin-layout",
    styleUrls: ["./ngx-admin-layout.component.scss"],
    template: `
        <ngx-one-column-layout>
            <nb-menu [items]="menu"></nb-menu>
            <router-outlet></router-outlet>
        </ngx-one-column-layout>
    `
})
export class NgxAdminLayoutComponent implements OnInit {
    constructor() { }
    menu = MENU_ITEMS;
    ngOnInit(): void { }
}

export const MENU_ITEMS: NbMenuItem[] = [

    {
        title: "Dashboard",
        icon: "home-outline",
        link: "/admin/dashboard"
    },
    {
        title: "People Management",
        icon: "people-outline",
        children: [
            {
                title: "Employees",
                link: "people/employees"
            },
            {
                title: "Attendances",
                link: "people/attendances"
            },
            {
                title: "Attendances Logs",
                link: "people/attendances-logs"
            },
            {
                title: "Overtimes",
                link: "people/overtimes"
            },
            {
                title: "My Queue",
                pathMatch: "prefix",
                link: "people/my-queue"
            }
        ]
    },
    {
        title: "Store Management",
        icon: "settings-outline",
        children: [
            {
                title: "Store Config",
                link: "people/employees"
            },
        ]
    },

];
