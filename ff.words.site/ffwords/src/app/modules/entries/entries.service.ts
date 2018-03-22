import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { AppService } from '../../app.service';

@Injectable()
export class EntriesService extends AppService {

    constructor(http: Http) {
        super(http);
    }

}
