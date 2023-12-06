import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SearchParams } from 'src/app/core/models/Filters/SearchParams';
import { AgGridBaseComponent } from 'src/app/core/shared/components/ag-grid-base/ag-grid-base.component';
import { CustomAction } from 'src/app/core/shared/components/table/custom-action';
import { OrdersService } from '../../../services/orders.service';

@Component({
  selector: 'app-city-correction',
  templateUrl: './city-correction.component.html',
  styleUrls: ['./city-correction.component.scss']
})
export class CityCorrectionComponent implements OnInit {
  cityCorrentionColumns: any[] = [];
  searchParams = new SearchParams();
  orderData: any[] = [];
  operationalCity: any[] = [];
  constructor(private orderService: OrdersService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadLookup();
    this.getOrders();
  }

  loadLookup() {
    this.orderService.getOperationalCityLookup().subscribe((res) => {
      this.operationalCity = res;
      this.operationalCity = this.operationalCity.filter(x => x.canDeliver == true);
    });
  }

  getOrders() {
    this.searchParams.pageNumber = 0
    this.searchParams.pageSize = 100;
    this.orderService.getCityCorrectionOrder().subscribe(res => {
      this.orderData = res.data;
    })
  }

  applyChanges() {
    console.log(this.orderData)
  }
}
