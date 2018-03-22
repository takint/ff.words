import { Http, Response, RequestOptionsArgs, RequestMethod, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

export abstract class AppService {
    constructor(private _http: Http) { }

    public getData(sourceUrl: string, params?: any): Observable<Response> {
        return this._http.get(sourceUrl, params);
    }

    public postData(url: string, data: any, params?: any): Observable<Response> {
        return this._http.post(url, data, params);
    }
}
