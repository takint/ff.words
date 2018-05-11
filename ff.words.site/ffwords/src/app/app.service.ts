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
    HttpErrorResponse
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
            //.map(response => parseResponse(response))
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

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.getToken()}` // refresh token
            }
        });

        return next.handle(request);
    //.do (
    //        (event: HttpEvent<any>) => {
    //            if (event instanceof HttpResponse) {
    //                // process the response
    //            }
    //        },
    //        (error: any) => {
    //            if (error instanceof HttpErrorResponse) {
    //                if (error.status === 401) { // Unauthorize
    //                    this.collectFailedRequestes(request);
    //                }
    //            }
    //        });
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
