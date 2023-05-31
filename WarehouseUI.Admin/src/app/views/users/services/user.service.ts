import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PagedResult } from '../../shared/models/pagination';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'user/';

  constructor(private http: HttpClient) {}

  getAll(fitlers): Observable<PagedResult<any[]>> {

    let params = new HttpParams();
    const paginatedResult: PagedResult<any[]> = new PagedResult<any[]>();
    params = params.append('pageNumber', fitlers.pageNumber != null ? fitlers.pageNumber .toString() :  null );

    params = params.append('pageSize',  fitlers.pageSize != null ? fitlers.pageSize .toString() :  null);
    if (fitlers.criteria)  {
      params = params.append('email', fitlers.criteria);
    }
   
    if (fitlers.plantId)  {
      params = params.append('plantId', fitlers.plantId);
    }
   

    return this.http.get(this.baseUrl , { observe: 'response', params})
    .pipe(
      map(response => {
      
          paginatedResult.entity = response.body['users'];
          paginatedResult.filters.totalItems = response.body['totalRecords'];
          return paginatedResult;
      }));
  }

  getUserById(id): Observable<any> {
    return this.http.get<any>(this.baseUrl + id);
  }

  update( user) {
    return this.http.put(this.baseUrl, user);
  }

  delete( id) {
    return this.http.delete(this.baseUrl + id);
  }

}
