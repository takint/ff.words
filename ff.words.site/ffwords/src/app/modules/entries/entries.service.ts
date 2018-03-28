import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AppService } from '../../app.service';
import { AppUtils } from '../../shared/shared.utils';

@Injectable()
export class EntriesService extends AppService {

    private listApi: string = 'Entry/GetEntries';
    private getApi: string = 'Entry/GetEntry/';
    private postApi: string = '';

    constructor(http: Http) {
        super(http);
    }

    public getList(): Promise<any> {
        return this.getData(`${AppUtils.apiHost}${this.listApi}`).toPromise<any>();
    }

    public getEntry(entryId: number): Promise<any> {
        return this.getData(`${AppUtils.apiHost}${this.getApi}${entryId}`).toPromise<any>();
    }
}
