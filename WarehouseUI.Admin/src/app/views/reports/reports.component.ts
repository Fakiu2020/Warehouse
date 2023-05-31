import { Component, OnInit } from '@angular/core';
import { FilterBase } from '../shared/models/pagination';
import { ReportService } from './report.services';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.scss']
})
export class ReportsComponent implements OnInit {
  separations = [];
  filters = new FilterBase();
  constructor(private reportService: ReportService) { }
  bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  reportsType = [
    {
      id: 1,
      description: 'Gráficos' 
    },

    {
      id: 2,
      description: 'Tabular' 
    },
  ];

  entitiesType = [
    {
      id: 1,
      description: 'Separaciones' 
    }
  ];
  

  ngOnInit(): void {
  }

  reportTypeChange(){

  }

  clearFilters(){

  }

  search() {
    
    //  this.isLoading = true;
      this.reportService.getAll(this.filters).subscribe(data => {
        this.separations = data.entity;
        // this.filters.totalItems = data.filters.totalItems;
        // this.isLoading = false;
      }, error => {
        // this.isLoading = false;
        // console.error(error);
      });
    
  }

}
