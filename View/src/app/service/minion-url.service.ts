import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Url } from 'src/models/url.model';

@Injectable({
  providedIn: 'root'
})
export class MinionUrlService {

	baseUrl = 'https://localhost:7206/api/MinionUrl/'

	constructor(private http: HttpClient) { }

	//Get all URLs
	getAllUrls(): Observable<Url[]> {
		return this.http.get<Url[]>(this.baseUrl+'getAllUrls');
	}
	getSingleUrl(): Observable<Url> {
		return this.http.get<Url>(this.baseUrl+'getSingleUrl')
	}
	addUrl(url : Url): Observable<Url> {
		return this.http.post<Url>(this.baseUrl+'addUrl', url)
	}
}
