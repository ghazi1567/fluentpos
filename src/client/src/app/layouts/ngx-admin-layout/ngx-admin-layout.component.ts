import { Component, OnInit } from "@angular/core";
import { NbMenuItem } from "@nebular/theme";

@Component({
    selector: "app-ngx-admin-layout",
    styleUrls: ["./ngx-admin-layout.component.scss"],
    template: `
        <ngx-one-column-layout>
            <nb-menu expanded="false" [items]="menu"></nb-menu>
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
        title: "Catalog Management",
        icon: "settings-outline",
        children: [
            {
                title: "Products",
                link: "catalog/products"
            },
        ]
    },
    {
        title: "Order Management",
        icon: "settings-outline",
        children: [
            {
                title: "Orders",
                link: "sales/orders"
            },
            {
                title: "City Correction",
                link: "sales/city-correction"
            },
            {
                title: "Confirm Order",
                link: "sales/confirm-order"
            },
            {
                title: "Load Sheets",
                link: "sales/load-sheets"
            },
        ]
    },
    {
        title: "Warehouses",
        icon: "settings-outline",
        children: [
            {
                title: "Locations",
                link: "sales/locations"
            },
            {
                title: "Inventory Import",
                link: "sales/inventory-import-files"
            },
            {
                title: "Stock Report",
                link: "sales/stock-report"
            },
        ]
    },
    {
        title: "Organization",
        icon: "settings-outline",
        children: [
            {
                title: "Store Config",
                link: "org/store"
            },
        ]
    },
    {
        title: "User Management",
        icon: "settings-outline",
        children: [
            {
                title: "Users",
                link: "identity/users"
            },
            {
                title: "Roles",
                link: "identity/roles"
            },
            {
                title: "Event Logs",
                link: "event-logs"
            },
        ]
    },

];
