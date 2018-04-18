import { Http, Response, RequestOptionsArgs, RequestMethod, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

export abstract class AppService extends BehaviorSubject<any> {
    constructor(private _http: Http) {
        super(null);
    }

    public getData(sourceUrl: string, params?: any): Observable<Response> {
        return this._http.get(sourceUrl, params)
            .map(response => parseResponse(response))
            .catch(ex => {
                console.log(ex);
                return Observable.throw(ex);
            });
    }

    public postData(url: string, data: any, params?: any): Observable<Response> {
        return this._http.post(url, data, params)
            .map(response => parseResponse(response))
            .catch(ex => {
                console.log(ex);
                return Observable.throw(ex);
            });
    }
}

export function parseResponse(response, isObject: boolean = false) {
    let jsonResponse;

    try {
        jsonResponse = isObject ? response : <any>response.json();
        for (let item in jsonResponse) {
            if (jsonResponse[item] !== null) {
                if (typeof (jsonResponse[item]) == "object") {
                    parseResponse(jsonResponse[item], true);
                }
            }
        }
    } catch (ex) {
        return null;
    }

    return jsonResponse;
}
