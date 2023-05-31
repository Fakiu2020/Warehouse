import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment';
import { PagedResult } from '../../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  baseApiUrl = environment.apiUrl + 'Warehouse/';

  constructor(private http: HttpClient) {}


  getAll(filters): Observable<PagedResult<any[]>> {

    let params = new HttpParams();
    const paginatedResult: PagedResult<any[]> = new PagedResult<any[]>();
    params = params.append('pageNumber', filters.page != null ? filters.page .toString() :  null );
    params = params.append('pageSize',  filters.pageSize != null ? filters.pageSize .toString() :  null);
 
    if(filters.criteria) {
      params = params.append('name', filters.criteria);
    }
    

    return this.http.get(this.baseApiUrl , { observe: 'response', params})
    .pipe(
      map(response => {
          paginatedResult.entity = response.body['products'];
          paginatedResult.filters.totalItems = response.body['totalRecords'];
          return paginatedResult;
      }));
  }

  getById(id): Observable<any> {
    return this.http.get<any>(this.baseApiUrl + id);
  }

  update(warehouse) {
    return this.http.put(this.baseApiUrl, warehouse);
  }

  delete(id) {
    return this.http.delete(this.baseApiUrl + id);
  }

  create(warehouse) {
    return this.http.post(this.baseApiUrl, warehouse);
  }

  calculate(lat,long): Observable<any> {
    return this.http.get<any>(this.baseApiUrl + 'calculate/' + lat + "/" + long);
  }

  getDetailAddressLocation(address): Observable<any> {
    return this.http.get<any>(this.baseApiUrl + 'GetDetailAddressLocation/' + address);
  }

  exporToExcel(params) {
  
    return this.http.post(this.baseApiUrl + 'ProductExportExcel', params, { responseType: 'blob'});
  }
}
