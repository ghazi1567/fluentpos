import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { ThemeModule } from 'src/app/theme/theme.module';


@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MaterialModule,
    SharedModule,
    ThemeModule
  ]
})
export class AuthModule { }
