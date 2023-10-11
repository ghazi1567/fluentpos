import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ThemeModule } from 'src/app/theme/theme.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TranslateModule,
    SharedModule,
    ThemeModule
  ]
})
export class SettingsModule { }
