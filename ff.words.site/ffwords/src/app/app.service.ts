import {
    Response,
    RequestOptionsArgs,
    RequestMethod,
    Headers
} from '@angular/http';
import {
    HttpClient,
    HttpRequest,
    HttpResponse,
    HttpInterceptor,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse,
    HttpSentEvent,
    HttpHeaderResponse,
    HttpProgressEvent,
    HttpUserEvent
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

export abstract class AppService extends BehaviorSubject<any> {
    constructor(private _http: HttpClient) {
        super(null);
    }

    public getData(sourceUrl: string, params?: any): Observable<HttpResponse<any>> {
        return this._http.get(sourceUrl, params)
            .map(response => parseResponse(response))
            .catch(ex => {
                console.log(ex);
                return Observable.throw(ex);
            });
    }

    public postData(url: string, data: any, params?: any): Observable<HttpResponse<any>> {
        return this._http.post(url, data, params)
            .map(response => parseResponse(response))
            .catch(ex => {
                console.log(ex);
                return Observable.throw(ex);
            });
    }
}

export class JwtInterceptor implements HttpInterceptor {

    private cachedRequests: Array<HttpRequest<any>> = [];

    intercept(request: HttpRequest<any>, next: HttpHandler):
        Observable<HttpSentEvent |
        HttpHeaderResponse |
        HttpProgressEvent |
        HttpResponse<any> |
        HttpUserEvent<any>> {

        return next.handle(this.addRequestToken(request)).catch(
            error => {
                if (error instanceof HttpErrorResponse) {
                    switch (error.status) {
                        case 400: // Not found
                            return;
                        case 401: // Unauthorize
                            this.collectFailedRequestes(request);
                            break;
                        default: break;
                    }
                } else {
                    return Observable.throw(error);
                }
            });
    }

    private addRequestToken(request: HttpRequest<any>): HttpRequest<any> {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.getToken()}` // refresh token
            }
        });
    }

    private collectFailedRequestes(rq: HttpRequest<any>): void {
        this.cachedRequests.push(rq);
    }

    private retryFailedRequests(): void {

    }

    private getToken(): string {
        return localStorage.getItem("access_token");
    }
}

export function parseResponse(response, isObject: boolean = false) {
    // For the first time the response is object itself
    if (typeof (response) === "object") {
        return response;
    }

    let jsonResponse;
    try {
        jsonResponse = isObject ? response : <any>response.json();
        
        for (let item in jsonResponse) {
            if (jsonResponse[item] !== null) {
                if (typeof (jsonResponse[item]) === "object") {
                    parseResponse(jsonResponse[item], true);
                }
            }
        }
    } catch (ex) {
        throw ex;
    }

    return jsonResponse;
}
