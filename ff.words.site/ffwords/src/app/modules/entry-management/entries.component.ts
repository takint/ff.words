import { Component, OnInit } from '@angular/core';
import { EntriesService } from './entries.service';
import { AppUtils } from '../../shared/shared.utils';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'app-entries',
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})
export class EntriesComponent implements OnInit {

    public promise: Promise<any> = null;

    constructor(private service: EntriesService) {
        this.promise = this.getList();
    }

    getList() {
        return this.service
            .getData(`${AppUtils.apiHost}api/Entry/GetEntries`)
            .toPromise<any>();
    }

    ngOnInit() {
    }

}
