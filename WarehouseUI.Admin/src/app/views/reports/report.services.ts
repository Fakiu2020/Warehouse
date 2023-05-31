import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { PagedResult } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  baseApiUrl = environment.apiUrl + 'Report/';

  constructor(private http: HttpClient) {}


  getAll(filters): Observable<PagedResult<any[]>> {

    let params = new HttpParams();
    const paginatedResult: PagedResult<any[]> = new PagedResult<any[]>();
    params = params.append('pageNumber', filters.page != null ? filters.page .toString() :  null );
    params = params.append('pageSize',  filters.pageSize != null ? filters.pageSize .toString() :  null);
    

    return this.http.get(this.baseApiUrl , { observe: 'response', params})
    .pipe(
      map(response => {
          paginatedResult.entity = response.body['SeparationResult'];
          paginatedResult.filters.totalItems = response.body['totalRecords'];
          return paginatedResult;
      }));
  }

 
}
