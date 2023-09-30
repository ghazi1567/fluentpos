import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrls: ['./auto-complete.component.scss']
})
export class AutoCompleteComponent implements OnInit {
  @Input() data: any[] = [];
  @Input() filterColumns: string[] = [];
  @Input() displayColumnId: string = '';
  @Input() displayColumnName: [] | string = '';
  @Input() control: FormControl;
  @Input() isObject: boolean = false;
  @Output() onSelection: any = new EventEmitter<any>();
  myControl = new FormControl();

  filteredOptions: Observable<any[]>;
  constructor() { }

  ngOnInit(): void {
    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      map((value) => (typeof value === 'object' ? this._filter(value.value) : this._filter(value)))
    );
  }

  onOptionSelection(event) {
    if (this.isObject == true) {
      this.control.setValue(this.myControl.value);
      this.onSelection.emit(event.option);
    } else {
      var _value = this.myControl.value;
      this.control.setValue(_value[this.displayColumnId]);
      this.onSelection.emit({
        value: _value[this.displayColumnId]
      });
    }
  }

  displayCustomerName(value) {
    if (typeof value === 'object' && value && this.displayColumnName) {
      if (Array.isArray(this.displayColumnName)) {
        var _displayValues = [];
        this.displayColumnName.forEach(x => {
          _displayValues.push(value[x]);
        });
        return _displayValues.join(' - ')
      } else {
        return value[this.displayColumnName];
      }
    } else {
      return value;
    }
  }
  displayOptions(value) {
    if (typeof value === 'object' && value && this.displayColumnName) {
      if (Array.isArray(this.displayColumnName)) {
        var _displayValues = [];
        this.displayColumnName.forEach(x => {
          _displayValues.push(value[x]);
        });
        return _displayValues.join(' - ')
      } else {
        return value[this.displayColumnName];
      }
    } else {
      return value;
    }
  }

  private _filter(value: string): any[] {
    if (!value) {
      return [];
    }

    const filterValue = value.toLowerCase();

    return this.data.filter(
      (option) => {
        var resultFound = false;
        this.filterColumns.forEach(col => {

          if (option[col]) {
            var isFound = option[col].toLowerCase().includes(filterValue);
            if (isFound == true) {
              resultFound = true;
            }
          }

        });
        return resultFound;
      }
    );
  }
}
