export interface TableColumn {
  name: string;
  dataKey: string;
  position?: 'right' | 'left';
  isSortable?: boolean;
  isShowable?: boolean;
  columnType?: string;
  format?: string;
  buttons?: string[];
  isClass?: boolean;
}
