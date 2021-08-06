import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EventLog } from 'src/app/core/models/event-logs/event-log';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { EventLogParams } from './models/eventLogParams';
import { EventLogService } from './services/event-log.service';
@Component({
  selector: 'app-event-logs',
  templateUrl: './event-logs.component.html',
  styleUrls: ['./event-logs.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class EventLogsComponent implements OnInit {

  eventLogs: PaginatedResult<EventLog>;
  eventLogColumns: string[] = ['id', 'email', 'messageType', 'timestamp', 'action'];
  eventLogParams = new EventLogParams();
  dataSource = new MatTableDataSource<EventLog>([]);
  searchString: string;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private eventLogService: EventLogService) { }

  ngOnInit(): void {
    this.getEventLogs();
  }
  public reload(): void {
    this.searchString = this.eventLogParams.searchString = '';
    this.eventLogParams.pageNumber = 0;
    this.eventLogParams.pageSize = 0;
    this.getEventLogs();
  }
  getEventLogs(): void {
    this.eventLogService.getEventLogs(this.eventLogParams).subscribe((result) => {
      this.eventLogs = result;
      this.dataSource.data = this.eventLogs.data;
    });
  }
  handlePageChange(event: PaginatedFilter): void {
    this.eventLogParams.pageNumber = event.pageNumber;
    this.eventLogParams.pageSize = event.pageSize;
    this.getEventLogs();
  }
  doSort(sort: Sort): void {
    this.eventLogParams.orderBy = sort.active + ' ' + sort.direction;
    this.getEventLogs();
  }

  public doFilter(): void {
    this.eventLogParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.eventLogParams.pageNumber = 0;
    this.eventLogParams.pageSize = 0;
    this.getEventLogs();
  }

}