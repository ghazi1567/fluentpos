import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
import { LicenseManager } from 'ag-grid-enterprise';
LicenseManager.setLicenseKey('CompanyName=SHI International Corp_on_behalf_of_Transatlantic Reinsurance Company,LicensedGroup=DEV2020,LicenseType=MultipleApplications,LicensedConcurrentDeveloperCount=3,LicensedProductionInstancesCount=0,AssetReference=AG-013099,ExpiryDate=19_May_2022_[v2]_MTY1MjkxNDgwMDAwMA==6883055779ea63036cf2f59c807231f4');

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
