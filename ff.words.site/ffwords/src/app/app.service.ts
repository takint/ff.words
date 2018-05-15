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

import { AppUtils } from './shared/shared.utils';

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

export class JwtInterceptor implements HttpInterceptor {

    private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
    private isRefreshingToken: boolean = localStorage.getItem("access_token") !== null;

    constructor(private _http: HttpClient) { }

    intercept(request: HttpRequest<any>, next: HttpHandler):
        Observable<HttpSentEvent |
        HttpHeaderResponse |
        HttpProgressEvent |
        HttpResponse<any> |
        HttpUserEvent<any>> {

        return next.handle(this.addRequestToken(request, this.getToken())).catch(
            error => {
                if (error instanceof HttpErrorResponse) {
                    switch (error.status) {
                        case 400: // Bad request
                            console.log("400");
                            return;
                        case 401: // Unauthorize
                            return this.processFailedRequestes(request, next);
                        default:
                            window.location.href = "Home/Login";
                    }
                } else {
                    return Observable.throw(error);
                }
            });
    }

    private refreshToken(): Observable<string> {
        let endPoint = 'http://localhost:31354/Home/GetUserAccessToken';

        this._http.get(endPoint).subscribe(
            (response: string) => {
                if (!AppUtils.isNullOrEmpty(response)) {
                    this.tokenSubject.next(response);
                    localStorage.setItem("access_token", response);
                }
            });

        return Observable.of(this.tokenSubject.value).delay(200);
    }

    private addRequestToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}` // refresh token
            }
        });
    }

    private processFailedRequestes(rq: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshingToken) {

            this.isRefreshingToken = true;
            this.tokenSubject.next(null);

            return this.refreshToken().switchMap(newToken => {
                if (!AppUtils.isNullOrEmpty(newToken)) {
                    return next.handle(this.addRequestToken(rq, this.tokenSubject.value));
                }

                // Logout if needed
            });
        } else {
            return next.handle(this.addRequestToken(rq, this.tokenSubject.value));
        }
    }

    private retryFailedRequests(): void {

    }

    private getToken(): string {
        return localStorage.getItem("access_token");
    }
}
