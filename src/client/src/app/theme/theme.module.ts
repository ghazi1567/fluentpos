import { ModuleWithProviders, NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FooterComponent, HeaderComponent, SearchInputComponent, OneColumnLayoutComponent } from "./components";
import { CapitalizePipe, PluralPipe, RoundPipe, TimingPipe, NumberWithCommasPipe } from "./pipes";
import {
    NbActionsModule,
    NbLayoutModule,
    NbMenuModule,
    NbSearchModule,
    NbSidebarModule,
    NbUserModule,
    NbContextMenuModule,
    NbButtonModule,
    NbSelectModule,
    NbIconModule,
    NbThemeModule,
    NbCardModule,
    NbCheckboxModule,
    NbInputModule,
    NbRadioModule,
    NbDatepickerModule,
    
} from "@nebular/theme";
import { NbEvaIconsModule } from "@nebular/eva-icons";
import { NbSecurityModule } from "@nebular/security";
import { DEFAULT_THEME } from "./styles/theme.default";
import { COSMIC_THEME } from "./styles/theme.cosmic";
import { CORPORATE_THEME } from "./styles/theme.corporate";
import { DARK_THEME } from "./styles/theme.dark";
import { NbAuthModule, NbDummyAuthStrategy } from "@nebular/auth";
import { FormsModule as ngFormsModule } from '@angular/forms';

const PIPES = [CapitalizePipe, PluralPipe, RoundPipe, TimingPipe, NumberWithCommasPipe];
const COMPONENTS = [FooterComponent, HeaderComponent, SearchInputComponent, OneColumnLayoutComponent];
const NB_MODULES = [
    NbLayoutModule,
    NbMenuModule,
    NbUserModule,
    NbActionsModule,
    NbSearchModule,
    NbSidebarModule,
    NbContextMenuModule,
    NbEvaIconsModule,
    NbButtonModule,
    NbSelectModule,
    NbIconModule,
    NbCardModule,
    NbCheckboxModule,
    NbInputModule,
    NbAuthModule,
    NbRadioModule,
    NbDatepickerModule,
    ngFormsModule
];
@NgModule({
    declarations: [...COMPONENTS, ...PIPES],
    imports: [CommonModule, ...NB_MODULES],
    exports: [...PIPES, ...COMPONENTS, ...NB_MODULES]
})
export class ThemeModule {
    static forRoot(): ModuleWithProviders<ThemeModule> {
        return {
            ngModule: ThemeModule,
            providers: [
                ...NbThemeModule.forRoot(
                    {
                        name: "default"
                    },
                    [DEFAULT_THEME, COSMIC_THEME, CORPORATE_THEME, DARK_THEME]
                ).providers,
                ...NbAuthModule.forRoot({

                    strategies: [
                        NbDummyAuthStrategy.setup({
                            name: 'email',
                            delay: 3000,
                        }),
                    ],
                    forms: {
                        login: {
                            socialLinks: [],
                        },
                        register: {
                            socialLinks: [],
                        },
                    },
                }).providers,
            ]
        };
    }
}
