import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  baseUrl = environment.apiUrl + 'roles/';

  constructor(private http: HttpClient) {}


  getAllRoles(): Observable<any> {
    return this.http.get(this.baseUrl);
  }
}
