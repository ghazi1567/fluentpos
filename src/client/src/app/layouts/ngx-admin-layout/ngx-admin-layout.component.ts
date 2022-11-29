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
    constructor() {}
    menu = MENU_ITEMS;
    ngOnInit(): void {}
}

export const MENU_ITEMS: NbMenuItem[] = [
   
    {
        title: "Dashboard",
        icon: "home-outline",
        link: "/ngx-admin/dashboard"
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
        title: "Forms",
        icon: "edit-2-outline",
        children: [
            {
                title: "Form Inputs",
                link: "/ngx-admin/forms/inputs"
            },
            {
                title: "Form Layouts",
                link: "/ngx-admin/forms/layouts"
            },
            {
                title: "Buttons",
                link: "/ngx-admin/forms/buttons"
            },
            {
                title: "Datepicker",
                link: "/ngx-admin/forms/datepicker"
            }
        ]
    },
    {
        title: "UI Features",
        icon: "keypad-outline",
        link: "/ngx-admin/ui-features",
        children: [
            {
                title: "Grid",
                link: "/ngx-admin/ui-features/grid"
            },
            {
                title: "Icons",
                link: "/ngx-admin/ui-features/icons"
            },
            {
                title: "Typography",
                link: "/ngx-admin/ui-features/typography"
            },
            {
                title: "Animated Searches",
                link: "/ngx-admin/ui-features/search-fields"
            }
        ]
    },
    {
        title: "Modal & Overlays",
        icon: "browser-outline",
        children: [
            {
                title: "Dialog",
                link: "/ngx-admin/modal-overlays/dialog"
            },
            {
                title: "Window",
                link: "/ngx-admin/modal-overlays/window"
            },
            {
                title: "Popover",
                link: "/ngx-admin/modal-overlays/popover"
            },
            {
                title: "Toastr",
                link: "/ngx-admin/modal-overlays/toastr"
            },
            {
                title: "Tooltip",
                link: "/ngx-admin/modal-overlays/tooltip"
            }
        ]
    },
    {
        title: "Extra Components",
        icon: "message-circle-outline",
        children: [
            {
                title: "Calendar",
                link: "/ngx-admin/extra-components/calendar"
            },
            {
                title: "Progress Bar",
                link: "/ngx-admin/extra-components/progress-bar"
            },
            {
                title: "Spinner",
                link: "/ngx-admin/extra-components/spinner"
            },
            {
                title: "Alert",
                link: "/ngx-admin/extra-components/alert"
            },
            {
                title: "Calendar Kit",
                link: "/ngx-admin/extra-components/calendar-kit"
            },
            {
                title: "Chat",
                link: "/ngx-admin/extra-components/chat"
            }
        ]
    },
    {
        title: "Maps",
        icon: "map-outline",
        children: [
            {
                title: "Google Maps",
                link: "/ngx-admin/maps/gmaps"
            },
            {
                title: "Leaflet Maps",
                link: "/ngx-admin/maps/leaflet"
            },
            {
                title: "Bubble Maps",
                link: "/ngx-admin/maps/bubble"
            },
            {
                title: "Search Maps",
                link: "/ngx-admin/maps/searchmap"
            }
        ]
    },
    {
        title: "Charts",
        icon: "pie-chart-outline",
        children: [
            {
                title: "Echarts",
                link: "/ngx-admin/charts/echarts"
            },
            {
                title: "Charts.js",
                link: "/ngx-admin/charts/chartjs"
            },
            {
                title: "D3",
                link: "/ngx-admin/charts/d3"
            }
        ]
    },
    {
        title: "Editors",
        icon: "text-outline",
        children: [
            {
                title: "TinyMCE",
                link: "/ngx-admin/editors/tinymce"
            },
            {
                title: "CKEditor",
                link: "/ngx-admin/editors/ckeditor"
            }
        ]
    },
    {
        title: "Tables & Data",
        icon: "grid-outline",
        children: [
            {
                title: "Smart Table",
                link: "/ngx-admin/tables/smart-table"
            },
            {
                title: "Tree Grid",
                link: "/ngx-admin/tables/tree-grid"
            }
        ]
    },
    {
        title: "Miscellaneous",
        icon: "shuffle-2-outline",
        children: [
            {
                title: "404",
                link: "/ngx-admin/miscellaneous/404"
            }
        ]
    },
    {
        title: "Auth",
        icon: "lock-outline",
        children: [
            {
                title: "Login",
                link: "/auth/login"
            },
            {
                title: "Register",
                link: "/auth/register"
            },
            {
                title: "Request Password",
                link: "/auth/request-password"
            },
            {
                title: "Reset Password",
                link: "/auth/reset-password"
            }
        ]
    }
];
