import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { ToastrService } from 'ngx-toastr';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { TableColumn } from 'src/app/core/shared/components/table/table-column';
import { Branch } from '../../models/branch';
import { SearchParams } from '../../models/SearchParams';
import { BranchService } from '../../services/branch.service';
import { branchFormComponent } from './branch-form/branch-form.component';

@Component({
  selector: 'app-branch',
  templateUrl: './branch.component.html',
  styleUrls: ['./branch.component.scss']
})
export class BranchComponent implements OnInit {
  branchs: PaginatedResult<Branch>;
  branchColumns: TableColumn[];
  branchParams = new SearchParams();
  searchString: string;

  constructor(
    public branchService: BranchService,
    public dialog: MatDialog,
    public toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getBranchs();
    this.initColumns();
  }

  
  getBranchs(): void {
    this.branchService.getAlls(this.branchParams).subscribe((result) => {
      this.branchs = result;
    });
  }

  initColumns(): void {
    this.branchColumns = [
      { name: 'Id', dataKey: 'id', isSortable: true, isShowable: true },
      { name: 'Name', dataKey: 'name', isSortable: true, isShowable: true },
      { name: 'Address', dataKey: 'address', isSortable: true, isShowable: true },
      { name: 'Action', dataKey: 'action', position: 'right' },
    ];
  }

  pageChanged(event: PaginatedFilter): void {
    this.branchParams.pageNumber = event.pageNumber;
    this.branchParams.pageSize = event.pageSize;
    this.getBranchs();
  }

  openForm(Branch?: Branch): void {
    const dialogRef = this.dialog.open(branchFormComponent, {
      data: Branch,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.getBranchs();
      }
    });
  }

  remove($event: string): void {
    this.branchService.delete($event).subscribe(() => {
      this.getBranchs();
      this.toastr.info('Branch Removed');
    });
  }

  sort($event: Sort): void {
    this.branchParams.orderBy = $event.active + ' ' + $event.direction;
    console.log(this.branchParams.orderBy);
    this.getBranchs();
  }

  filter($event: string): void {
    this.branchParams.searchString = $event.trim().toLocaleLowerCase();
    this.branchParams.pageNumber = 0;
    this.branchParams.pageSize = 0;
    this.getBranchs();
  }

  reload(): void {
    this.branchParams.searchString = '';
    this.branchParams.pageNumber = 0;
    this.branchParams.pageSize = 0;
    this.getBranchs();
  }
}
