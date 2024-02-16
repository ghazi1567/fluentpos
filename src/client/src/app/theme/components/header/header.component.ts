import { Component, OnDestroy, OnInit } from "@angular/core";
import { NbMediaBreakpointsService, NbMenuService, NbSidebarService, NbThemeService } from "@nebular/theme";

import { filter, map, takeUntil } from "rxjs/operators";
import { Subject } from "rxjs";
import { LayoutService } from "src/app/core/utils";
import { AuthService } from "src/app/core/services/auth.service";

@Component({
    selector: "ngx-header",
    styleUrls: ["./header.component.scss"],
    templateUrl: "./header.component.html"
})
export class HeaderComponent implements OnInit, OnDestroy {
    private destroy$: Subject<void> = new Subject<void>();
    userPictureOnly: boolean = false;
    user: any = {
        name: "Inam Ul Haq",
        picture: ""
    };

    themes = [
        {
            value: "default",
            name: "Light"
        },
        {
            value: "dark",
            name: "Dark"
        },
        {
            value: "cosmic",
            name: "Cosmic"
        },
        {
            value: "corporate",
            name: "Corporate"
        }
    ];

    currentTheme = "default";

    userMenu = [{ title: "Profile" }, { title: "Log out" }];

    constructor(
        private sidebarService: NbSidebarService,
        private menuService: NbMenuService,
        private themeService: NbThemeService,
        private layoutService: LayoutService,
        private breakpointService: NbMediaBreakpointsService,
        private authService: AuthService
    ) {}

    ngOnInit() {
        this.currentTheme = this.themeService.currentTheme;

        const { xl } = this.breakpointService.getBreakpointsMap();
        this.themeService
            .onMediaQueryChange()
            .pipe(
                map(([, currentBreakpoint]) => currentBreakpoint.width < xl),
                takeUntil(this.destroy$)
            )
            .subscribe((isLessThanXl: boolean) => (this.userPictureOnly = isLessThanXl));

        this.themeService
            .onThemeChange()
            .pipe(
                map(({ name }) => name),
                takeUntil(this.destroy$)
            )
            .subscribe((themeName) => (this.currentTheme = themeName));
        this.toggleSidebar();

        this.menuService
            .onItemClick()
            .pipe(
                filter(({ tag }) => tag === "my-user-menu"),
                map(({ item: { title } }) => title)
            )
            .subscribe((title) => {
                if (title == "Log out") {
                    this.authService.logout();
                }
            });
    }

    ngOnDestroy() {
        this.destroy$.next();
        this.destroy$.complete();
    }

    changeTheme(themeName: string) {
        this.themeService.changeTheme(themeName);
    }

    toggleSidebar(): boolean {
        this.sidebarService.toggle(true, "menu-sidebar");
        this.layoutService.changeLayoutSize();

        return false;
    }

    navigateHome() {
        this.menuService.navigateHome();
        return false;
    }
}
